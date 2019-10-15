using EvansSnackMachine.Logic.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace EvansSnackMachine.Persistence.Models
{
    public class SnackMachineDBO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MoneyDBO MoneyInside { get; set; }
        public decimal AmountInTransaction { get; set; }
        public IList<SlotDBO> Slots { get; set; }

        public SnackMachineDBO()
        {
            this.MoneyInside = new MoneyDBO();
            this.AmountInTransaction = 0;
            this.Slots = new List<SlotDBO>();
        }

        public SnackMachineDBO(SnackMachine machine)
        {
            this.Id = machine.Id;
            this.MoneyInside = new MoneyDBO(machine.MoneyInside);
            this.AmountInTransaction = machine.AmountInTransaction;
            this.Slots = machine.GetSlots()
                .Select(slot => new SlotDBO(slot))
                .ToList();
        }
    }
}
