DocumentDB storage provider for Orleans
===============================

This is a StorageProvider for DocumentDB for the Microsoft Research Project Orleans. The storage provider allows to persist stateful grains into DocumentDB. 

In order to use the provider you need to add the following provider to your configuration:

````
<Provider Type="Orleans.StorageProvider.DocumentDB.DocumentDBStorageProvider" Name="DocumentDBStore" Url="https://<account>.documents.azure.com:443/" Key="<key>" />
````
and this line to any grain that uses it
````
[StorageProvider(ProviderName = "DocumentDBStore")]
````

You can obtain the Url and Key from Keys blade in Azure Portal's DocumentDB Account.