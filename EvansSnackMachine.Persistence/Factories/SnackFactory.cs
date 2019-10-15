using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Persistence.Models;

namespace EvansSnackMachine.Persistence.Factories
{
    public static class SnackFactory
    {
        public static Snack Build(SnackDBO dbo)
        {
            return new Snack(dbo.Name);
        }
    }
}
