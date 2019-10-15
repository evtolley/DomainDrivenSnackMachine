using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.ValueObjects;

namespace EvansSnackMachine.Persistence.Models
{
    public class SnackPileDBO
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public SnackDBO Snack { get; set; }

        public SnackPileDBO(SnackPile snackPile)
        {
            this.Snack = new SnackDBO(snackPile.Snack);
            this.Quantity = snackPile.Quantity;
            this.Price = snackPile.Price;
        }
    }
}
