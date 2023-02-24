using POC1.Domain.Entities;

namespace POC1.Application.Interfaces
{
    public interface IApiBlobsQueryRepository
    {
        Task<ApiBlob> DownloadAsync(string blobFilename);
    }
}
