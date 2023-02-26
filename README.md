## API
This Api has two endpoints

1. Logs , which will fetch the records for Api calling to public api and its corresponding blob Name which is returned from public api.
2. payload, it will give the content of blob dumped in the Azure container against provided Log id, retrieved from previous endpoint.  
  
### Configurations:

There are three config settings that we can change depending upon the azure storage we want it to connect.  
AzureStorageConnectionString    : [Your Azure Connection String]  
  BlobContainerName             : [Your Azure Container Name]  
  AzureTableName		            : [Your Azure Table Name]
  
## Azure Function 

Runs after every 1 minute and make a http request to asked URL. Logs http response code along with date time and responce payload if response code is 200. Incase of other response codes (other than 200); will not store the payload in blob storage but time and code in table storage.
  
  
### Configurations:  
  
Azure function also required to update these three below Keys in envoirnment veriable(s)  
  AzureWebJobsStorage   : [Your Azure Connection String]  
  StorageTableName      : [Your Azure Table Name]  
  StorageBlobName       : [Your Azure Container Name]
  
