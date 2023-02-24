using AutoMapper;
using POC1.Application.Dtos;
using POC1.Domain.Entities;

namespace POC1.Application.Common
{
    public class ApiLogProfile : Profile
    {
        
        public ApiLogProfile()
        {

            CreateMap<ApiLogs, ApiLogsList>()
                .ForMember(dest => dest.ApiLogId, opt => opt.MapFrom(src => src.PartitionKey))
                .ForMember(dest => dest.BlobName, opt => opt.MapFrom(src => src.PayloadBlobFileName))
                .ForMember(dest => dest.ResponseCode, opt => opt.MapFrom(src => src.ResponseCode));
            CreateMap<ApiBlob, ApiBlobResponse>();
        }
    }
}
