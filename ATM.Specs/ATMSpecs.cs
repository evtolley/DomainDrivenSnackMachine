using ATM.Logic;
using FluentAssertions;
using SharedKernel.ValueObjects;
using Xunit;

namespace ATM.Tests
{
    public class ATMSpecs
    {
        [Fact]
        public void Take_Money_Should_Exchange_Money_With_Commission()
        {
            var atm = new AutomatedTellerMachine();
            atm.LoadMoney(Money.Dollar);

            atm.TakeMoney(1.00m);
            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(1.01m);
        }

        [Fact]
        public void Commission_Should_Be_At_Least_One_Cent()
        {
            var atm = new AutomatedTellerMachine();
            atm.LoadMoney(Money.OneCent);

            atm.TakeMoney(.01m);
            atm.MoneyCharged.Should().Be(.02m);
        }

        [Fact]
        public void Commission_Should_Be_Rounded_Up_To_Nearest_Cent()
        {
            var atm = new AutomatedTellerMachine();
            atm.LoadMoney(Money.FiveDollar);
            atm.LoadMoney(Money.Quarter);

            atm.TakeMoney(.25m);
            atm.MoneyCharged.Should().Be(.26m);
        }
    }
}
