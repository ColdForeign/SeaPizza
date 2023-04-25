using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Persistence.Initialization;

public interface ICustomSeeder
{
    Task InitializeAsync(CancellationToken cancellationToken);
}
