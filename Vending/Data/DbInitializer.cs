using Vending.Models;

namespace Vending.Data
{
    public class DbInitializer
    {
        public static void Initialize(VendingMachineContext context)
        {
            if (context.Database.EnsureCreated())
            {
                var drinks = new List<Drink>
            {
                new Drink { Name = "Cola", Price = 5.0m, Quantity = 10 },
                new Drink { Name = "Water", Price = 1.0m, Quantity = 5 },
                new Drink { Name = "Orange Juice", Price = 2.0m, Quantity = 8 },
            };

                context.Drinks.AddRange(drinks);
                context.SaveChanges();
            }
        }
    }
}
