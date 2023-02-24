using MediatR;
using POC1.Application.Dtos;

namespace POC1.Application.Queries.ApiLogs
{
    public class GetApiLogsQuery : IRequest<ApiLogsResponse>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}