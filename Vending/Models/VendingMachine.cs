namespace Vending.Models
{
    public class VendingMachine
    {
        public int Id { get; set; }
        public decimal DepositedAmount { get; set; }
        public List<Drink> Drinks { get; set; }

        public VendingMachine()
        {
            Drinks = new List<Drink>(); 
        }

    }
}
