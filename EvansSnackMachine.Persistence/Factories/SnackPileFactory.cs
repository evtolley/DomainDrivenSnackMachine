using EvansSnackMachine.Logic.ValueObjects;
using EvansSnackMachine.Persistence.Models;

namespace EvansSnackMachine.Persistence.Factories
{
    public static class SnackPileFactory
    {
        public static SnackPile Build(SnackPileDBO dbo)
        {
            return new SnackPile(
                SnackFactory.Build(dbo.Snack),
                dbo.Quantity,
                dbo.Price
                );
        }
    }
}
