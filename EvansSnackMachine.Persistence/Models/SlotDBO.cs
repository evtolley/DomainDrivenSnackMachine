using EvansSnackMachine.Logic.Entities;

namespace EvansSnackMachine.Persistence.Models
{
    public class SlotDBO
    {
        public int Position { get; set; }
        public SnackPileDBO SnackPile { get; set; }

        public SlotDBO(Slot slot)
        {
            this.Position = slot.Position;
            this.SnackPile = new SnackPileDBO(slot.SnackPile);
        }
    }
}
