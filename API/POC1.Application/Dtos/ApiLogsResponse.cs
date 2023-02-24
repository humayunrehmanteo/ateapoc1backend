namespace POC1.Application.Dtos
{
    public class ApiLogsList
    {
        public string? ApiLogId { get; set; }
        public string? ResponseCode { get; set; }
        public string? BlobName { get; set; }
    }
    public class ApiLogsResponse
    {
        public List<ApiLogsList>? ApiLogs { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }

    }
}
