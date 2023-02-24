This Api has two endpoints

1. Logs , which will fetch the records for Api calling to public api and its corresponding blob Name which is returned from public api.
2. payload, it will give the content of blob dumped in the Azure container against provided Log id, retrieved from previous endpoint.


Configurations:

There are three config settings that we can change depending upon the azure storage we want it to connect.
		AzureStorageConnectionString : <Your Azure Connection String>
		BlobContainerName            : <Your Azure Container Name>
		AzureTableName		     : <Your Azure Table Name>



Architecture:

We have used Clean Architecture Microservices Patteren in this Api.
Also used Automapper for mapping entities to Dtos.
Mediatr for achieving CQRS.

