using EvansSnackMachine.Logic.ValueObjects;
using EvansSnackMachine.Persistence.Entities;

namespace EvansSnackMachine.Logic.Factories
{
    public static class MoneyFactory
    {
        public static Money Build(MoneyDBO dbo)
        {
            return new Money(
                dbo.OneCentCount, 
                dbo.TenCentCount, 
                dbo.QuarterCount, 
                dbo.OneDollarCount, 
                dbo.FiveDollarCount, 
                dbo.TwentyDollarCount);
        }
    }
}
