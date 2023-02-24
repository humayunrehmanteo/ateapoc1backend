using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using POC1.Application.Interfaces;
using POC1.Domain.Entities;
using System.Text;

namespace POC1.Infrastructure.Repositories
{
    public class ApiBlobsQueryRepository : IApiBlobsQueryRepository
    {
        private readonly string? _storageConnectionString;
        private readonly string? _storageContainerName;
        public ApiBlobsQueryRepository(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetValue<string>("AzureStorageConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blobFilename"></param>
        /// <returns>ApiBlob object with its content, name and content type</returns>

        public async Task<ApiBlob> DownloadAsync(string blobFilename)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {

                BlobClient blobClient = client.GetBlobClient(blobFilename);

                if (await blobClient.ExistsAsync())
                {
                    var data = await blobClient.OpenReadAsync();

                    var response = blobClient.DownloadContent();

                    var dataContent = response.Value.Content;
                    var blobContents = Encoding.UTF8.GetString(dataContent);

                    string name = blobFilename;
                    string contentType = response.Value.Details.ContentType;

                    return new ApiBlob { Content = blobContents, Name = name, ContentType = contentType };
                }
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {

                throw ;
            }
            return new ApiBlob();
        }

    }
}
