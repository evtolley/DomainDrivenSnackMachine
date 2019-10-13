using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Persistence.Entities;

namespace EvansSnackMachine.Logic.Factories
{
    public static class SnackMachineFactory
    { 
        public static SnackMachine Build(SnackMachineDBO dbo)
        {
            return new SnackMachine(
                dbo.Id,
                MoneyFactory.Build(dbo.MoneyInside),
                dbo.AmountInTransaction
                );
        }
    }
}
