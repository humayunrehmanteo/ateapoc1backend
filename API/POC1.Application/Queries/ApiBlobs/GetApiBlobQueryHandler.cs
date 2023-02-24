using AutoMapper;
using MediatR;
using POC1.Application.Dtos;
using POC1.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace POC1.Application.Queries.ApiBlobs
{
    public class GetApiBlobQueryHandler : IRequestHandler<GetApiBlobQuery, ApiBlobResponse>
    {
        private readonly IApiBlobsQueryRepository _apiBlobsQueryRepository;
        private readonly IApiLogsQueryRepository _apiLogsQueryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetApiBlobQueryHandler> _logger;
        public GetApiBlobQueryHandler(IApiBlobsQueryRepository apiBlobsQueryRepository, IApiLogsQueryRepository apiLogsQueryRepository, IMapper mapper, ILogger<GetApiBlobQueryHandler> logger)
        {
            _apiBlobsQueryRepository = apiBlobsQueryRepository;
            _apiLogsQueryRepository = apiLogsQueryRepository;
            _mapper = mapper;
            _logger = logger;  
        }

        public async Task<ApiBlobResponse> Handle(GetApiBlobQuery request, CancellationToken cancellationToken)
        {
            try
            {
                
                if (request.LogId != null)
                {
                    var apiLog = _apiLogsQueryRepository.GetApiLog(request.LogId);
                    if (apiLog.PayloadBlobFileName != null)
                    {
                        var apiBlob = await _apiBlobsQueryRepository.DownloadAsync(apiLog.PayloadBlobFileName);

                        var blob = _mapper.Map<ApiBlobResponse>(apiBlob);
                        if (blob != null)
                        {
                            blob.Success = true;
                            blob.Message = "Success";
                            return blob;
                        }

                    }
                    return new ApiBlobResponse()
                    {
                        Success = false, Message = "No Blob found against the logId"
                    };

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ApiBlobResponse()
                {
                    Success = false,
                    Message = ex.Message
                };
            }

            return new ApiBlobResponse()
            {
                Success = false,
                Message = "LogId not provided"
            };

        }
    }
}
