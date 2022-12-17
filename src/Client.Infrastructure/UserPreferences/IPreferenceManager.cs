using SeaPizza.Client.Infrastructure.Common;
using System.Threading.Tasks;

namespace SeaPizza.Client.Infrastructure.UserPreferences;

public interface IPreferenceManager : IAppService
{
    Task SetPreference(IPreference preference);

    Task<IPreference>GetPreference ();
}
