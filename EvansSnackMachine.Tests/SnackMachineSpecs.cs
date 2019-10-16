using System;
using Xunit;
using FluentAssertions;
using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.ValueObjects;
using EvansSnackMachine.Logic.Exceptions;
using System.Collections.Generic;
using SharedKernel.ValueObjects;

namespace EvansSnackMachine.Tests
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void Returning_Money_Should_Empty_Money_In_Transaction()
        {

            SnackMachine machine = new SnackMachine();

            machine.InsertMoney(Money.Quarter);
            machine.InsertMoney(Money.Quarter);
            machine.InsertMoney(Money.Quarter);
            machine.InsertMoney(Money.Quarter);

            machine.ReturnMoney();

            Assert.Equal(0, machine.AmountInTransaction);
        }

        [Fact]
        public void Inserted_Money_Goes_To_Money_In_Transaction()
        {

            SnackMachine machine = new SnackMachine();

            machine.InsertMoney(Money.Dollar);
            machine.InsertMoney(Money.OneCent);
            machine.InsertMoney(Money.Quarter);


            Assert.Equal(1.26m, machine.AmountInTransaction);
        }

        [Fact]
        public void Cannot_Insert_More_Than_One_Coin_Or_Note_At_A_Time()
        {

            SnackMachine machine = new SnackMachine();

            var invalidValue = Money.OneCent + Money.FiveDollar;
            Action action = () => machine.InsertMoney(invalidValue);


            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Money_In_Transaction_Goes_To_Money_Inside_After_Purchase()
        {

            SnackMachine machine = new SnackMachine();
            machine.LoadSnacks(1, new SnackPile(new Snack("Hee haw"), 5, 6));
            machine.InsertMoney(Money.OneCent);
            machine.InsertMoney(Money.Dollar);
            machine.InsertMoney(Money.FiveDollar);

            machine.BuySnack(1);

            machine.AmountInTransaction.Should().Be(0);
            Assert.Equal(6, machine.MoneyInside.Amount);
        }

        [Fact]
        public void Parameterless_Constructor_Should_Work()
        {

            SnackMachine machine = new SnackMachine();

            machine.AmountInTransaction.Should().Be(0);
            machine.MoneyInside.Should().Be(Money.None);
        }

        [Fact]
        public void Money_Constructor_Should_Work()
        {
            var slots = new List<Slot>() { new Slot(2) };
            SnackMachine machine = new SnackMachine("12345", Money.Dollar, 5m, slots);

            machine.Id.Should().Be("12345");
            machine.MoneyInside.Should().Be(Money.Dollar);
            machine.AmountInTransaction.Should().Be(5m);
            machine.Slots.Count.Should().Be(1);
        }

        [Fact]
        public void Buy_Snacks_Trades_Inserted_Money_For_A_Snack()
        {

            SnackMachine machine = new SnackMachine();
            machine.LoadSnacks(1, new SnackPile(new Snack("Delicious Snack"), 4, 1));
            machine.InsertMoney(Money.Dollar);
            machine.BuySnack(1);

            machine.AmountInTransaction.Should().Be(0m);
            machine.MoneyInside.Amount.Should().Be(1);

            machine.GetSnackPile(1).Quantity.Should().Be(3);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(5)]
        public void Cannot_Call_Buy_Snack_With_An_Invalid_Position(int position)
        {
            SnackMachine machine = new SnackMachine();

            Action action = () => machine.BuySnack(position);


            action.Should().Throw<InvalidOperationException>();
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(5)]
        public void Cannot_Call_Load_Snack_With_An_Invalid_Position(int position)
        {
            SnackMachine machine = new SnackMachine();

            Action action = () => machine.LoadSnacks(position, new SnackPile(new Snack("Test snack"), 5, 6));


            action.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(5)]
        public void Cannot_Call_Get_Snack_Pile_With_An_Invalid_Position(int position)
        {
            SnackMachine machine = new SnackMachine();

            Action action = () => machine.GetSnackPile(position);


            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Calling_Buy_Snack_When_There_Are_No_Snacks_In_Position_Should_Throw_Error()
        {
            SnackMachine machine = new SnackMachine();

            Action action = () => machine.BuySnack(1);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_Make_Purchase_If_There_Isnt_Enough_Money_Inserted()
        {
            SnackMachine machine = new SnackMachine();
            machine.LoadSnacks(1, new SnackPile(new Snack("Delicious Snack"), 4, .75m));
            machine.InsertMoney(Money.Quarter);

            Action action = () => machine.BuySnack(1);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Snack_Machine_Should_Give_Highest_Denomination_Back_First()
        {
            SnackMachine machine = new SnackMachine();
            machine.LoadMoney(Money.Dollar);

            machine.InsertMoney(Money.Quarter);
            machine.InsertMoney(Money.Quarter);
            machine.InsertMoney(Money.Quarter);
            machine.InsertMoney(Money.Quarter);

            machine.ReturnMoney();

            machine.MoneyInside.QuarterCount.Should().Be(4);
            machine.MoneyInside.OneDollarCount.Should().Be(0);
        }


        [Fact]
        public void Change_Should_Be_Returned_After_Purchase()
        {
            SnackMachine machine = new SnackMachine();
            machine.LoadMoney(new Money(0,0,3,0,0,0));

            machine.LoadSnacks(1, new SnackPile(new Snack("Tasty"), 400, .75m));

            machine.InsertMoney(Money.Quarter);
            machine.InsertMoney(Money.Quarter);
            machine.InsertMoney(Money.Dollar);

            machine.BuySnack(1);

            machine.MoneyInside.Amount.Should().Be(1.50m);
        }

        [Fact]
        public void Should_Not_Be_Able_To_Purchase_If_Machine_Cant_Make_Change()
        {
            SnackMachine machine = new SnackMachine();
            machine.LoadMoney(Money.Dollar);
            machine.LoadSnacks(1, new SnackPile(new Snack("Tasty"), 400, .75m));

            machine.InsertMoney(Money.Quarter);
            machine.InsertMoney(Money.Quarter);
            machine.InsertMoney(Money.Dollar);

            Action action = () => machine.BuySnack(1);
            action.Should().Throw<CannotMakeChangeException>();
        }
    }
}
