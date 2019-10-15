using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Persistence.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvansSnackMachine.Persistence.Models
{
    public class SlotDBO
    {
        public int Position { get; set; }
        public SnackPileDBO SnackPile { get; set; }

        public SlotDBO(Slot slot)
        {
            this.Position = slot.Position;
            this.SnackPile = new SnackPileDBO(slot.SnackPile);
        }
    }
}
