using Microsoft.EntityFrameworkCore;
using Vending.Models;

namespace Vending.Data
{
    public class VendingMachineContext : DbContext
    {
        public VendingMachineContext(DbContextOptions<VendingMachineContext> options) : base(options)
        {
        }

        public DbSet<VendingMachine> VendingMachines { get; set; }
        public DbSet<Drink> Drinks { get; set; }
    }
}
