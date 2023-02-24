using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using POC1.Application.Interfaces;
using POC1.Domain.Entities;

namespace POC1.Infrastructure.Repositories
{
    /// <summary>
    /// This class contains the methods for getting Api Logs with specific Date range filter
    /// </summary>
    public class ApiLogsQueryRepository : IApiLogsQueryRepository
    {
        private readonly TableClient _service;

        public ApiLogsQueryRepository(IConfiguration configuration)
        {
            var storageConnectionString = configuration.GetValue<string>("AzureStorageConnectionString");
            var azureTableName = configuration.GetValue<string>("AzureTableName");
            _service = new TableServiceClient(storageConnectionString).GetTableClient(azureTableName);
        }

        /// <summary>
        /// Get Logs from Azure table within the time range provided 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>List of Api Logs between time range</returns>
        /// <exception cref="Exception"></exception>
        public List<ApiLogs> GetApiLogs(DateTime from, DateTime to)
        {
            try
            {
                var dateRangeFilter = TableQuery.CombineFilters(
                TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.GreaterThanOrEqual, from),
                TableOperators.And,
                TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThan, to));

                var result = _service.Query<ApiLogs>(filter: dateRangeFilter).ToList();

                return result;

            }
            catch 
            {
                throw;
            }

        }
        /// <summary>
        /// Gets the specific log detail against the provided log id
        /// </summary>
        /// <param name="logId"></param>
        /// <returns>Api Log</returns>
        /// <exception cref="Exception"></exception>
        public ApiLogs GetApiLog(string logId)
        {
            try
            {
                var keyfilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, logId);
                var result = _service.Query<ApiLogs>(filter: keyfilter).FirstOrDefault();

                return result ?? new();
            }
            catch
            {

                throw ;
            }
        }

    }
}
