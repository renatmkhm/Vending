using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vending.Data;
using Vending.Models;

namespace Vending.Controllers
{
    public class AdminInterfaceController : Controller
    {
        private readonly VendingMachineContext _context;
        private const string AdminSecretKey = "admin";

        public AdminInterfaceController(VendingMachineContext context)
        {
            _context = context;
        }

        public IActionResult Index(string secretkey)
        {
            if (secretkey != AdminSecretKey)
            {
                return Unauthorized(); 
            }
            var vendingMachine = _context.VendingMachines.Include(vm => vm.Drinks).FirstOrDefault();
            return View(vendingMachine);
        }

        [HttpPost]
        public IActionResult AddDrink(Drink drink)
        {
            var vendingMachine = _context.VendingMachines.Include(vm => vm.Drinks).FirstOrDefault();
            vendingMachine.Drinks.Add(drink);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteDrink(int drinkId)
        {
            var vendingMachine = _context.VendingMachines.Include(vm => vm.Drinks).FirstOrDefault();
            var drink = vendingMachine.Drinks.FirstOrDefault(d => d.Id == drinkId);

            if (drink != null)
            {
                vendingMachine.Drinks.Remove(drink);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateDrink(Drink drink)
        {
            var vendingMachine = _context.VendingMachines.Include(vm => vm.Drinks).FirstOrDefault();
            var existingDrink = vendingMachine.Drinks.FirstOrDefault(d => d.Id == drink.Id);

            if (existingDrink != null)
            {
                existingDrink.Name = drink.Name;
                existingDrink.Price = drink.Price;
                existingDrink.Quantity = drink.Quantity;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
