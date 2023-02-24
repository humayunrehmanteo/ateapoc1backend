using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using POC1.Application.Dtos;
using POC1.Application.Interfaces;

namespace POC1.Application.Queries.ApiLogs
{
    public class GetApiLogsQueryHandler : IRequestHandler<GetApiLogsQuery, ApiLogsResponse>
    {
        private readonly IApiLogsQueryRepository _iApiLogsQueryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetApiLogsQueryHandler> _logger;
        public GetApiLogsQueryHandler(IApiLogsQueryRepository apiLogsQueryRepository, IMapper mapper, ILogger<GetApiLogsQueryHandler> logger)
        {
            _iApiLogsQueryRepository = apiLogsQueryRepository;
            _mapper = mapper;
            _logger = logger;   
        }
        public async Task<ApiLogsResponse> Handle(GetApiLogsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = _iApiLogsQueryRepository.GetApiLogs(request.From, request.To);
                if (response.Count > 0)
                {

                    var apiLogs = await Task.FromResult(_mapper.Map<List<ApiLogsList>>(response));

                    return new ApiLogsResponse()
                    {
                        ApiLogs = apiLogs,
                        Success = true
                    };
                }
                return new ApiLogsResponse(){ Success = false,Message = "No logs found in this time range" };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ApiLogsResponse()
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }
    }
}
