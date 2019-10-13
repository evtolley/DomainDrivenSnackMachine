using EvansSnackMachine.Logic.ValueObjects;

namespace EvansSnackMachine.Logic.Entities
{
    public class Slot : Entity
    {
        public int Position { get; private set; }
        public SnackMachine SnackMachine { get; private set; }
        public SnackPile SnackPile { get; set; }


        public Slot(SnackMachine snackMachine, Snack snack, int position)
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = new SnackPile(null, 0, 0m);
        }
    }
}
