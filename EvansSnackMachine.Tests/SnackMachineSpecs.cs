﻿using EvansSnackMachine.Logic;
using System;
using Xunit;
using FluentAssertions;

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

            Assert.Equal(0, machine.MoneyInTransaction.Amount);
        }

        [Fact]
        public void Inserted_Money_Goes_To_Money_In_Transaction()
        {

            SnackMachine machine = new SnackMachine();

            machine.InsertMoney(Money.Dollar);
            machine.InsertMoney(Money.OneCent);
            machine.InsertMoney(Money.Quarter);


            Assert.Equal(1.26m, machine.MoneyInTransaction.Amount);
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
            machine.InsertMoney(Money.OneCent);
            machine.InsertMoney(Money.Dollar);
            machine.InsertMoney(Money.FiveDollar);

            machine.BuySnack();

            machine.MoneyInTransaction.Should().Be(Money.None);
            Assert.Equal(6.01m, machine.MoneyInside.Amount);
        }
    }
}