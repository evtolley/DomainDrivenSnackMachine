using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Persistence.Models;

namespace EvansSnackMachine.Persistence.Factories
{
    public static class SlotFactory
    {
        public static Slot Build(SlotDBO dbo)
        {
            return new Slot(
                dbo.Position,
                SnackPileFactory.Build(dbo.SnackPile)
                );
        }
    }
}
