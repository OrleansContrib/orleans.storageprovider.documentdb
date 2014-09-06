using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using Orleans.Runtime;
using Orleans.Storage;

namespace Orleans.StorageProvider.DocumentDB
{
    class DocumentDBStorageProvider : IStorageProvider
    {
        public string Name { get; set; }
        public OrleansLogger Log { get; set; }

        private DocumentClient Client { get; set; }
        private Database Database { get; set; }

        public async Task Init(string name, Providers.IProviderRuntime providerRuntime, Providers.IProviderConfiguration config)
        {
            try
            {
                var url = config.Properties["Url"];
                var key = config.Properties["Key"];
                var databaseName = config.Properties["Database"];
                
                this.Client = new DocumentClient(new Uri(url), key);

                var databases = await this.Client.ReadDatabaseFeedAsync();
                this.Database = databases.Where(d => d.Id == databaseName).FirstOrDefault()
                    ?? await this.Client.CreateDatabaseAsync(new Database { Id = databaseName });
            }
            catch (Exception ex)
            {
                Log.Error(0, "Error in Init.", ex);
            }
        }

        public Task Close()
        {
            this.Client.Dispose();

            return TaskDone.Done;
        }

        public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            try
            {
                var collection = await this.EnsureCollection(grainType);
                var documents = await this.Client.ReadDocumentFeedAsync(collection.DocumentsLink);
                var documentId = grainReference.ToKeyString();
                GrainStateDocument document = documents.Where(d => d.Id == documentId).FirstOrDefault();

                if(document != null)
                    grainState.SetAll(document.State);
            }
            catch (Exception ex)
            {
                Log.Error(0, "Error in ReadStateAsync", ex);
            }            
        }

        public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            try
            {
                var collection = await this.EnsureCollection(grainType);
                var documents = await this.Client.ReadDocumentFeedAsync(collection.DocumentsLink);
                var documentId = grainReference.ToKeyString();

                var document = documents.Where(d => d.Id == documentId).FirstOrDefault();

                if(document != null)
                {
                    document.State = grainState.AsDictionary();
                    await this.Client.ReplaceDocumentAsync(document);
                }
                else
                {
                    await this.Client.CreateDocumentAsync(collection.DocumentsLink,
                        new GrainStateDocument { Id = documentId, State = grainState.AsDictionary() });
                }
            }
            catch (Exception ex)
            {
                Log.Error(0, "Error in WriteStateAsync", ex);
            }
        }

        public async Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            try
            {
                var collection = await this.EnsureCollection(grainType);
                var document = this.Client.CreateDocumentQuery(collection.DocumentsLink).Where(d => d.Id == grainReference.ToKeyString()).AsEnumerable().FirstOrDefault();

                if(document != null)
                    await this.Client.DeleteDocumentAsync(document.SelfLink);
            }
            catch (Exception ex)
            {
                Log.Error(0, "Error in ClearStateAsync", ex);
            }
        }

        private async Task<DocumentCollection> EnsureCollection(string collectionId)
        {
            var collections = await this.Client.ReadDocumentCollectionFeedAsync(this.Database.CollectionsLink);
            
            return collections.Where(c => c.Id == collectionId).FirstOrDefault()
                ?? await this.Client.CreateDocumentCollectionAsync(this.Database.SelfLink, new DocumentCollection { Id = collectionId });
        }

        private class GrainStateDocument
        {
            [JsonProperty("id")]
            public string Id;
            public IDictionary<string, object> State;
        }
    }
}