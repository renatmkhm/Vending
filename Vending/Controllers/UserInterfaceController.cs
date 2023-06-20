using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vending.Data;
using Vending.Models;

namespace Vending.Controllers
{
    public class UserInterfaceController : Controller
    {
        private readonly VendingMachineContext _context;

        public UserInterfaceController(VendingMachineContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vendingMachine = _context.VendingMachines.Include(vm => vm.Drinks).FirstOrDefault();
            if (vendingMachine == null)
            {
                vendingMachine = new VendingMachine();
                _context.VendingMachines.Add(vendingMachine);
                _context.SaveChanges();
            }
            return View(vendingMachine);
        }

        [HttpPost]
        public IActionResult Deposit(decimal amount)
        {
            var vendingMachine = _context.VendingMachines.Include(vm => vm.Drinks).FirstOrDefault();
            vendingMachine.DepositedAmount += amount;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SelectDrink(int drinkId)
        {
            var vendingMachine = _context.VendingMachines.Include(vm => vm.Drinks).FirstOrDefault();
            var drink = vendingMachine.Drinks.FirstOrDefault(d => d.Id == drinkId);

            if (drink != null && drink.Quantity > 0 && drink.Price <= vendingMachine.DepositedAmount)
            {
                vendingMachine.DepositedAmount -= drink.Price;
                drink.Quantity--;
                _context.SaveChanges();
                return Json(new { success = true, message = "Drink purchased successfully." });
            }

            return Json(new { success = false, message = "Unable to purchase drink." });
        }

        [HttpPost]
        public IActionResult GetChange()
        {
            var vendingMachine = _context.VendingMachines.FirstOrDefault();
            var change = vendingMachine.DepositedAmount;
            vendingMachine.DepositedAmount = 0;
            _context.SaveChanges();
            return Json(change);
        }
    }
}
