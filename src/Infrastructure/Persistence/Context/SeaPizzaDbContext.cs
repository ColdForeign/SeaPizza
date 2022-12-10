using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Persistence.Context
{
    public class SeaPizzaDbContext : BaseDbContext
    {
        public SeaPizzaDbContext (DbContextOptions options, IOptions<DatabaseSettings> dbSettings)
            : base(options, dbSettings)
        {
        }

    }
}
