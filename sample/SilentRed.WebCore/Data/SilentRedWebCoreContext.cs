using Microsoft.EntityFrameworkCore;
using Studiekring.Business.Customers.Commands;

namespace SilentRed.WebCore.Data
{
    public class SilentRedWebCoreContext : DbContext
    {
        public SilentRedWebCoreContext (DbContextOptions<SilentRedWebCoreContext> options)
            : base(options)
        {
        }

        public DbSet<NewCustomerCommand> NewCustomerCommand { get; set; }
    }
}
