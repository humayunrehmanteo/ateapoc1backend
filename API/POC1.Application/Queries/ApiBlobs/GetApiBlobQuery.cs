using MediatR;
using POC1.Application.Dtos;

namespace POC1.Application.Queries.ApiBlobs
{
    public class GetApiBlobQuery : IRequest<ApiBlobResponse>
    {
        public string? LogId { get; set; }
    }
}
