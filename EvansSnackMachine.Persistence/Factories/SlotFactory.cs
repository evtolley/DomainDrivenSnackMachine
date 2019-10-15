using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.Factories;
using EvansSnackMachine.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvansSnackMachine.Persistence.Factories
{
    public static class SlotFactory
    {
        public static Slot Build(SlotDBO dbo)
        {
            return new Slot(
                dbo.Position,
                SnackPileFactory.Build(dbo.SnackPile)
                );
        }
    }
}
