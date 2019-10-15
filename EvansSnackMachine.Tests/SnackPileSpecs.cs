using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace EvansSnackMachine.Tests
{
    public class SnackPileSpecs
    {
        [Theory]
        [InlineData(-1, .89)]
        [InlineData(5, -.89)]
        [InlineData(5, .9884374)]
        public void Initializing_With_Invalid_Quantity_Or_Price_Should_Throw_Exception(int quantity, decimal price)
        {
            Action action = () => new SnackPile(new Snack("Tasty snack"), quantity, price);


            action.Should().Throw<InvalidOperationException>();
        }
    }
}
