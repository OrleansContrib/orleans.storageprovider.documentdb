using Orleans.Runtime.Configuration;
using System.Collections.Generic;

namespace Orleans.StorageProvider.DocumentDB
{
    public static class ExtensionMethods
    {
        public static void RegisterDocumentDBProvider(this GlobalConfiguration configuration, string providerName, string url, string key, string database, string collection)
        {
            var config = new Dictionary<string, string> {
                {"Url", url },
                {"Key", key },
                {"Database", database },
                {"Collection", collection},
            };
            configuration.RegisterStorageProvider<DocumentDBStorageProvider>(providerName, config);
        }
    }
}
