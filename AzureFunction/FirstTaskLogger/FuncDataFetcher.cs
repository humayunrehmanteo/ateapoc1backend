using System;
using System.Net.Http;
using System.Threading.Tasks;
using FirstTaskLogger.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FirstTaskLogger
{
    public class FuncDataFetcher
    {
        private readonly IExtractInfoService _extractInfoService;
        public FuncDataFetcher(IExtractInfoService extractInfoService)
        {
            _extractInfoService = extractInfoService;
        }

        [FunctionName("FuncDataFetcher")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            try
            {
                await _extractInfoService.GetStoreLogDataAsync();
            }
            catch (Exception ex)
            {
                log.LogInformation($"Exception: {ex.Message} on {DateTime.Now}");
            }
        }
    }
}

