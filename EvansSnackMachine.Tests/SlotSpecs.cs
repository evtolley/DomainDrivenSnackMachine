using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EvansSnackMachine.Tests
{
    public class SlotSpecs
    {
        [Fact]
        public void Single_Constructor_Should_Work()
        {

            Slot slot = new Slot(2);
            slot.Position.Should().Be(2);
            slot.SnackPile.Snack.Should().Be(null);
        }

        [Fact]
        public void Factory_Constructor_Should_Work()
        {
            Slot slot = new Slot(2, new SnackPile(new Snack("Hee hee"), 3, 5));
            slot.Position.Should().Be(2);
            slot.SnackPile.Snack.Name.Should().Be("Hee hee");
        }
    }
}
