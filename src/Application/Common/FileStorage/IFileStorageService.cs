using SeaPizza.Application.Common.Interfaces;
using SeaPizza.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Application.Common.FileStorage;

public interface IFileStorageService : ITransientService
{
    public Task<string> UploadAsync<T>(FileUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    where T : class;

    public void Remove(string? path);
}