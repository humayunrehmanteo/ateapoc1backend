using POC1.Domain.Entities;

namespace POC1.Application.Interfaces;

public interface IApiLogsQueryRepository
{
    public List<ApiLogs> GetApiLogs(DateTime from, DateTime to);
    public ApiLogs GetApiLog(string logId);
}
