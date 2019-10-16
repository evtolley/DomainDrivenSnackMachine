using EvansSnackMachine.Logic.ValueObjects;
using SharedKernel;

namespace EvansSnackMachine.Logic.Entities
{
    public class Slot : Entity
    {
        public int Position { get; private set; }
        public SnackPile SnackPile { get; set; }


        public Slot(int position)
        {
            Position = position;
            SnackPile = new SnackPile(null, 0, 0m);
        }

        public Slot(int position, SnackPile snackPile)
        {
            this.Position = position;
            this.SnackPile = snackPile;
        }
    }
}
