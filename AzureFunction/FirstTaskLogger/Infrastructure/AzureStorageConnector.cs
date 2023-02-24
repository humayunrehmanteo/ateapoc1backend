using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace FirstTaskLogger.Infrastructure
{
    public class AzureStorageConnector : IAzureStorageConnector
    {
        class FirstTaskTable : TableEntity
        {
            public string ResponseCode { get; set; }
            public string PayloadBlobFileName { get; set; }

        }
        public async Task<string> StorePayloadDataBlobAsync(string fileContent)
        {
            string fileName = "response_" + Guid.NewGuid().ToString() + ".txt";

            try
            {
                await UploadFileInBlobContainerAsync(fileName, fileContent);

                return fileName;
            }
            catch
            {
                return string.Empty;
            }
        }
        public async Task<bool> InsertTableLogAsync(string responseCode, string payloadBlobFileName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configurations.AzureStorage.ConnectionString.Value);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(Configurations.AzureStorage.TableName.Value);
            await table.CreateIfNotExistsAsync();

            var newRow = new FirstTaskTable
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = DateTime.UtcNow.ToString(),
                ResponseCode = responseCode,
                PayloadBlobFileName = payloadBlobFileName
            };

            var insertResponse = await table.ExecuteAsync(TableOperation.Insert(newRow));

            return
                insertResponse.HttpStatusCode == ((int)System.Net.HttpStatusCode.OK) ||
                insertResponse.HttpStatusCode == ((int)System.Net.HttpStatusCode.NoContent);
        }

        #region Private Methods
        private async Task<bool> UploadFileInBlobContainerAsync(string fileName, string fileContent)
        {
            BlobContainerClient container = new BlobContainerClient(Configurations.AzureStorage.ConnectionString.Value,
                Configurations.AzureStorage.ContainerName.Value);

            await container.CreateIfNotExistsAsync();

            BlobClient blob = container.GetBlobClient(fileName);
            var binaryContent = Encoding.UTF8.GetBytes(fileContent);

            var ms = new MemoryStream(binaryContent);
            await blob.UploadAsync(ms);

            return true;
        }

        #endregion
    }
}
