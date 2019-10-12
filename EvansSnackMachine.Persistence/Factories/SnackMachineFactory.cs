using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvansSnackMachine.Logic.Factories
{
    public static class SnackMachineFactory
    { 
        public static SnackMachine Build(SnackMachineDBO dbo)
        {
            return new SnackMachine(
                dbo.Id,
                MoneyFactory.Build(dbo.MoneyInside),
                MoneyFactory.Build(dbo.MoneyInTransaction)
                );
        }
    }
}
