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
        public MoneyDBO MoneyInTransaction { get; set; }

        public SnackMachineDBO()
        {
            MoneyInside = new MoneyDBO();
            MoneyInTransaction = new MoneyDBO();
        }

        public SnackMachineDBO(SnackMachine machine)
        {
            Id = machine.Id;
            MoneyInside = new MoneyDBO(machine.MoneyInside);
            MoneyInTransaction = new MoneyDBO(machine.MoneyInTransaction);
        }
    }
}
