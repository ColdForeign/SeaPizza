using SeaPizza.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Application.Common.Mailing;

public interface IMailService : ITransientService
{
    Task SendAsync(MailRequest request, CancellationToken ct);
}
