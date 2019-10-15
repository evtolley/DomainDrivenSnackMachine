using EvansSnackMachine.Logic.ValueObjects;

namespace EvansSnackMachine.Persistence.Models
{
    public class MoneyDBO
    {
        public int OneCentCount { get; set; }
        public int TenCentCount { get; set; }
        public int QuarterCount { get; set; }
        public int OneDollarCount { get; set; }
        public int FiveDollarCount { get; set; }
        public int TwentyDollarCount { get; set; }

        public MoneyDBO()
        {
        }

        public MoneyDBO(Money money)
        {
            OneCentCount = money.OneCentCount;
            TenCentCount = money.TenCentCount;
            QuarterCount = money.QuarterCount;
            OneDollarCount = money.OneDollarCount;
            FiveDollarCount = money.FiveDollarCount;
            TwentyDollarCount = money.TwentyDollarCount;
        }
    }
}
