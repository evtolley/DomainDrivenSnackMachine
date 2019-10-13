using EvansSnackMachine.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvansSnackMachine.Logic
{
    public abstract class AggregateRoot : Entity
    {
        public AggregateRoot()
        {
        }

        public AggregateRoot(string id) : base(id)
        {
        }
    }
}
