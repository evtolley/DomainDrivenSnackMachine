using EvansSnackMachine.Logic.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EvansSnackMachine.Persistence.Entities
{
    public class SnackMachineDBO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MoneyDBO MoneyInside { get; set; }
        public decimal AmountInTransaction { get; set; }

        public SnackMachineDBO()
        {
            MoneyInside = new MoneyDBO();
            AmountInTransaction = 0;
        }

        public SnackMachineDBO(SnackMachine machine)
        {
            Id = machine.Id;
            MoneyInside = new MoneyDBO(machine.MoneyInside);
            AmountInTransaction = machine.AmountInTransaction;
        }
    }
}
