using Azure;
using Azure.Data.Tables;

namespace POC1.Domain.Entities
{
    public class ApiLogs : ITableEntity
    {
        public string? ResponseCode { get; set; }
        public string? PayloadBlobFileName { get; set; }
        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}

