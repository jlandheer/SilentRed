using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Studiekring.Business.Customers.Commands;

namespace SilentRed.WebCore.Models
{
    public class SilentRedWebCoreContext : DbContext
    {
        public SilentRedWebCoreContext (DbContextOptions<SilentRedWebCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Studiekring.Business.Customers.Commands.NewCustomerCommand> NewCustomerCommand { get; set; }
    }
}
