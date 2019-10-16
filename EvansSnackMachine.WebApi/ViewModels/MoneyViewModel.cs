using SharedKernel.ValueObjects;

namespace EvansSnackMachine.WebApi.ViewModels
{
    public class MoneyViewModel
    {
        public int OneCentCount { get; set; }
        public int TenCentCount { get; set; }
        public int QuarterCount { get; set; }
        public int OneDollarCount { get; set; }
        public int FiveDollarCount { get; set; }
        public int TwentyDollarCount { get; set; }

        public Money ConvertToMoney()
        {
            return new Money(OneCentCount, TenCentCount, QuarterCount, OneDollarCount, FiveDollarCount, TwentyDollarCount);
        }
    }
}
