﻿using EvansSnackMachine.Logic.Entities;

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
