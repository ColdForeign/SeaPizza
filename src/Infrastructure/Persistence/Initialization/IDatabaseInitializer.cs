using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Persistence.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);
}
