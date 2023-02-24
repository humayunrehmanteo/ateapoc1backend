using FirstTaskLogger.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskLogger.Services
{
    public class ExtractInfoService : IExtractInfoService
    {

        private readonly HttpClient _httpClient;
        private readonly IAzureStorageConnector _azureStorageConnector;
        public ExtractInfoService(IHttpClientFactory httpClientFactory, IAzureStorageConnector azureStorageConnector)
        {
            _httpClient = httpClientFactory.CreateClient();
            _azureStorageConnector = azureStorageConnector;
        }
        public async Task<bool> GetStoreLogDataAsync()
        {
            if (_httpClient is null) throw new Exception(ServiceMessages.HttpClientNullObject.Value);
            if (_azureStorageConnector is null) throw new Exception(ServiceMessages.AzureStorageNullObject.Value);

            try
            {
                var response = await _httpClient.GetAsync(FuncConfigurations.DataURL.Value);

                string fileName = string.Empty;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var payloadJsonString = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(payloadJsonString))
                        fileName = await _azureStorageConnector.StorePayloadDataBlobAsync(payloadJsonString);

                }
                await _azureStorageConnector.InsertTableLogAsync(response.StatusCode.ToString(), fileName);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
