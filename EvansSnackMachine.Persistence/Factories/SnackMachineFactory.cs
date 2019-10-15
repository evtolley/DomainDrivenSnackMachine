using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Persistence.Factories;
using EvansSnackMachine.Persistence.Models;
using System.Collections.Generic;

namespace EvansSnackMachine.Logic.Factories
{
    public static class SnackMachineFactory
    { 
        public static SnackMachine Build(SnackMachineDBO dbo)
        {
            var slots = new List<Slot>();

            foreach(var slotDbo in dbo.Slots)
            {
                slots.Add(SlotFactory.Build(slotDbo));
            }

            return new SnackMachine(
                dbo.Id,
                MoneyFactory.Build(dbo.MoneyInside),
                dbo.AmountInTransaction,
                slots
                );
        }
    }
}
