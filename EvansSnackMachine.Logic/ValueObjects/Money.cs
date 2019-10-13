using System;

namespace EvansSnackMachine.Logic.ValueObjects
{
    public sealed class Money : ValueObject<Money>
    {
        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money OneCent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);

        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public decimal Amount
        {
            get 
            {
                return OneCentCount * .01m +
                    TenCentCount * .10m +
                    QuarterCount * .25m +
                    OneDollarCount +
                    FiveDollarCount * 5 +
                    TwentyDollarCount * 20;
            }
        }

        public Money(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount)
        {
            if(oneCentCount < 0 || tenCentCount < 0 || quarterCount < 0 || oneDollarCount < 0 || fiveDollarCount < 0 || twentyDollarCount < 0)
            {
                throw new InvalidOperationException();
            }

            this.OneCentCount = oneCentCount;
            this.TenCentCount = tenCentCount;
            this.QuarterCount = quarterCount;
            this.OneDollarCount = oneDollarCount;
            this.FiveDollarCount = fiveDollarCount;
            this.TwentyDollarCount = twentyDollarCount;
        }

        public Money AllocateToReturn(decimal amount)
        {
            int twentyDollarCount = Math.Min((int)(amount / 20), TwentyDollarCount);
            amount = amount - twentyDollarCount * 20;

            int fiveDollarCount = Math.Min((int)(amount / 5), FiveDollarCount);
            amount = amount - fiveDollarCount * 5;

            int dollarCount = Math.Min((int)(amount / 1), OneDollarCount);
            amount = amount - dollarCount;

            int quarterCount = Math.Min((int)(amount / .25m), QuarterCount);
            amount = amount - quarterCount * .25m;

            int tenCentCount = Math.Min((int)(amount / .10m), TenCentCount);
            amount = amount - tenCentCount * .10m;

            int oneCentCount = Math.Min((int)(amount / .01m), OneCentCount);
            amount = amount - oneCentCount * .01m;

            return new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                dollarCount,
                fiveDollarCount,
                twentyDollarCount
                );

        }

        public static Money operator +(Money money1, Money money2)
        {
            return new Money(
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount
                );
        }

        public static Money operator *(Money money1, int multiplier)
        {
            return new Money(
               money1.OneCentCount * multiplier,
               money1.TenCentCount * multiplier,
               money1.QuarterCount * multiplier,
               money1.OneDollarCount * multiplier,
               money1.FiveDollarCount * multiplier,
               money1.TwentyDollarCount * multiplier
               );
        }

        public static Money operator -(Money money1, Money money2)
        {
            return new Money(
                money1.OneCentCount - money2.OneCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.QuarterCount - money2.QuarterCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount,
                money1.TwentyDollarCount - money2.TwentyDollarCount
                );
        }

        protected override bool EqualsCore(Money other)
        {
            return this.OneCentCount == other.OneCentCount
                && this.TenCentCount == other.TenCentCount
                && this.QuarterCount == other.QuarterCount
                && this.OneDollarCount == other.OneDollarCount
                && this.FiveDollarCount == other.FiveDollarCount
                && this.TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashcode = OneCentCount;
                hashcode = (hashcode * 397) ^ TenCentCount;
                hashcode = (hashcode * 397) ^ QuarterCount;
                hashcode = (hashcode * 397) ^ OneDollarCount;
                hashcode = (hashcode * 397) ^ FiveDollarCount;
                hashcode = (hashcode * 397) ^ TwentyDollarCount;
                return hashcode;
            }
        }

        public override string ToString()
        {
            return $@"${Amount.ToString("0.00")}";
        }
    }
}
