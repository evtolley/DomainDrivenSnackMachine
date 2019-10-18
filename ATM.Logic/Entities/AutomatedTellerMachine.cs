using EvansSnackMachine.Logic;
using SharedKernel.ValueObjects;
using System;

namespace ATM.Logic.Entities
{
    public class AutomatedTellerMachine : AggregateRoot
    {
        private const decimal CommissionRate = .01m;
        public Money MoneyInside { get; private set; }
        public decimal MoneyCharged { get; private set; }

        public AutomatedTellerMachine()
        {
            this.MoneyInside = Money.None;
        }

        public AutomatedTellerMachine(decimal moneyCharged, Money moneyInside)
        {
            this.MoneyCharged = moneyCharged;
            this.MoneyInside = moneyInside;
        }

        public void TakeMoney(decimal amount)
        {
            if (CanTakeMoney(amount))
            {
                var moneyToReturn = MoneyInside.AllocateToReturn(amount);

                MoneyInside -= moneyToReturn;

                MoneyCharged += CalculateCommission(amount);
            }
        }

        private bool CanTakeMoney(decimal amount)
        {
            if(amount <= 0)
            {
                throw new InvalidOperationException();
            }

            if(MoneyInside.Amount < amount)
            {
                throw new InvalidOperationException();
            }

            if (!MoneyInside.CanAllocate(amount))
            {
                throw new InvalidOperationException();
            }

            return true;
        }

        public void LoadMoney(Money money)
        {
            MoneyInside += money;
        }

        private decimal CalculateCommission(decimal amount)
        {
            decimal commission = amount * CommissionRate;
            decimal lessThanCent = commission % .01m;

            // round the commission up to the nearest cent
            if(commission % .01m > 0)
            {
                commission = commission - lessThanCent + .01m; 
            }


            return Math.Round(amount + commission, 2);
        }
    }
}
