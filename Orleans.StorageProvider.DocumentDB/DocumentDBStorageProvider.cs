using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Orleans.Runtime;
using Orleans.Storage;
using System;
using System.Threading.Tasks;

namespace Orleans.StorageProvider.DocumentDB
{

    // TODO: Ensure collection exists
    // TODO: CI
    // TODO: Testing
    // TODO: Nuget
    // TODO: readme
    // TODO: extension method for registering the provider


    class DocumentDBStorageProvider : IStorageProvider
    {
        private string databaseName;
        private string collectionName;

        public string Name { get; set; }
        public Logger Log { get; set; }

        private DocumentClient Client { get; set; }

        public async Task Init(string name, Providers.IProviderRuntime providerRuntime, Providers.IProviderConfiguration config)
        {
            try
            {
                this.Name = name;
                var url = config.Properties["Url"];
                var key = config.Properties["Key"];
                this.databaseName = config.Properties["Database"];
                this.collectionName = config.Properties["Collection"];
                
                this.Client = new DocumentClient(new Uri(url), key);
            }
            catch (Exception ex)
            {
                Log.Error(0, "Error in Init.", ex);
                throw;
            }
        }

        public Task Close()
        {
            if (null != this.Client) this.Client.Dispose();

            return TaskDone.Done;
        }

        Uri GenerateUri(string grainType, GrainReference reference)
        {
            return UriFactory.CreateDocumentUri(databaseName, collectionName, reference.ToKeyString());
        }

        public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            try
            {

                var uri = GenerateUri(grainType, grainReference);
                Document readDoc = await this.Client.ReadDocumentAsync(uri);

                if (null != readDoc)
                {
                    grainState.ETag = readDoc.ETag;
                    grainState.State = readDoc.GetPropertyValue<object>("state");
                }

            }
            catch (Exception ex)
            {
                Log.Error(0, "Error in ReadStateAsync", ex);
                throw;
            }            
        }

        public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            try
            {
                var uri = GenerateUri(grainType, grainReference);
                var document = new Document();

                document.SetPropertyValue("state", grainState.State);

                if (null != grainState.ETag)
                {
                    var ac = new AccessCondition { Condition = grainState.ETag, Type = AccessConditionType.IfMatch };
                    await this.Client.ReplaceDocumentAsync(uri, document, new RequestOptions { AccessCondition = ac });
                }
                else
                {
                    Document newDoc = await this.Client.CreateDocumentAsync(uri, document);
                    grainState.ETag = newDoc.ETag;
                }

            }
            catch (Exception ex)
            {
                Log.Error(0, "Error in WriteStateAsync", ex);
                throw;
            }
        }

        public async Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            try
            {
                var uri = GenerateUri(grainType, grainReference);
                await this.Client.DeleteDocumentAsync(uri);
                grainState.State = null;
                grainState.ETag = null;
            }
            catch (Exception ex)
            {
                Log.Error(0, "Error in ClearStateAsync", ex);
                throw;
            }
        }

      

    }
}