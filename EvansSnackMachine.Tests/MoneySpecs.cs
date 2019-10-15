using EvansSnackMachine.Logic.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace EvansSnackMachine.Tests
{
    public class MoneySpecs
    {
        [Fact]       
        public void Sum_Of_Two_Moneys_Produces_Correct_Result()
        {
            Money money = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money sum = money + money2;

            Assert.Equal(2, sum.OneCentCount);
            Assert.Equal(4, sum.TenCentCount);
            Assert.Equal(6, sum.QuarterCount);
            Assert.Equal(8, sum.OneDollarCount);
            Assert.Equal(10, sum.FiveDollarCount);
            Assert.Equal(12, sum.TwentyDollarCount);
        }

        [Fact]
        public void Money_Objects_With_Same_Values_Should_Be_Equal()
        {
            Money money = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Assert.True(money == money2);
        }

        [Fact]
        public void Money_Objects_With_Different_Values_Should_Not_Be_Equal()
        {
            Money money = new Money(99, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Assert.True(money != money2);
        }

        [Fact]
        public void Money_Objects_With_Different_Values_Should_Not_Be_Equal2()
        {
            Money money = new Money(1, 99, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Assert.True(money != money2);
        }


        [Fact]
        public void Money_Objects_With_Different_Values_Should_Not_Be_Equal3()
        {
            Money money = new Money(1, 2, 99, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Assert.True(money != money2);
        }

        [Fact]
        public void Money_Objects_With_Different_Values_Should_Not_Be_Equal4()
        {
            Money money = new Money(1, 2, 3, 99, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Assert.True(money != money2);
        }

        [Fact]
        public void Money_Objects_With_Different_Values_Should_Not_Be_Equal5()
        {
            Money money = new Money(1, 2, 3, 4, 99, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Assert.True(money != money2);
        }

        [Fact]
        public void Money_Objects_With_Different_Values_Should_Not_Be_Equal6()
        {
            Money money = new Money(1, 2, 3, 4, 5, 99);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Assert.True(money != money2);
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -1, 0, 0, 0, 0)]
        [InlineData(0, 0, -1, 0, 0, 0)]
        [InlineData(0, 0, 0, -1, 0, 0)]
        [InlineData(0, 0, 0, 0, -1, 0)]
        [InlineData(0, 0, 0, 0, 0, -1)]
        public void Cannot_Create_Money_With_A_Negative_Value(int one, int two, int three, int four, int five, int six)
        {
            Action action = () => new Money(one, two, three, four, five, six);

            action.Should().Throw<InvalidOperationException>();       
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, .01)]
        [InlineData(1, 2, 3, 0, 0, 0, .96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
        public void Amount_Should_Be_Calculated_Correctly(int one, int two, int three, int four, int five, int six, decimal expectedValue)
        {
           var money = new Money(one, two, three, four, five, six);
            Assert.Equal(expectedValue, money.Amount);
        }

        [Fact]
        public void Subtracting_Two_Moneys_Should_Produce_Correct_Result()
        {
            Money money = new Money(10, 10, 10, 10, 10, 10);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money result = money - money2;

            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void Cannot_Subtract_More_Than_Exists()
        {
            Money money1 = Money.TenCent;
            Money money2 = Money.OneCent;

            Action action = () =>
            {
                Money money = money1 - money2;
            };

            action.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, "$0.00")]
        [InlineData(1, 0, 0, 0, 0, 0, "$0.01")]
        [InlineData(1, 2, 3, 0, 0, 0, "$0.96")]
        [InlineData(1, 2, 3, 4, 0, 0, "$4.96")]
        [InlineData(1, 2, 3, 4, 5, 0, "$29.96")]
        [InlineData(1, 2, 3, 4, 5, 6, "$149.96")]
        [InlineData(110, 0, 0, 0, 100, 0, "$501.10")]
        public void To_String_Should_Format_Correctly(int one, int two, int three, int four, int five, int six, string expectedValue)
        {
            var money = new Money(one, two, three, four, five, six);
            Assert.Equal(expectedValue, money.ToString());
        }

        [Fact]
        public void Allocate_To_Return_Should_Return_Largest_Denominations_Possible()
        {
            var money = new Money(425,1,3,4,5,6);
            var moneyToReturn = money.AllocateToReturn(4.25m);

            moneyToReturn.TwentyDollarCount.Should().Be(0);
            moneyToReturn.FiveDollarCount.Should().Be(0);
            moneyToReturn.OneDollarCount.Should().Be(4);
            moneyToReturn.QuarterCount.Should().Be(1);
            moneyToReturn.TenCentCount.Should().Be(0);
            moneyToReturn.OneCentCount.Should().Be(0);       
        }
    }
}
