using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.Interfaces;
using EvansSnackMachine.Persistence.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq;

namespace EvansSnackMachine.Persistence.Repositories
{

    public class SnackMachineRepository : ISnackMachineRepository
    {
        private readonly IMongoCollection<MongoObject<SnackMachine>> _snackMachines;

        public SnackMachineRepository(ISnackMachineDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _snackMachines = database.GetCollection<MongoObject<SnackMachine>>(settings.SnackMachineCollectionName);
        }

        public SnackMachine CreateSnackMachine()
        {
            var snackMachine = new MongoObject<SnackMachine>(new SnackMachine());
            _snackMachines.InsertOne(snackMachine);

            return snackMachine.Unwrap();
        }

        public SnackMachine GetSnackMachine(string id)
        {
            return _snackMachines.Find(Builders<MongoObject<SnackMachine>>.Filter.Eq("_id", ObjectId.Parse(id)))
                .FirstOrDefault()
                .Unwrap();
        }

        public SnackMachine UpdateSnackMachine(SnackMachine machine)
        {
            _snackMachines.UpdateOne(Builders<MongoObject<SnackMachine>>
                .Filter.Eq("_id", ObjectId.Parse(machine.Id)),
                Builders<MongoObject<SnackMachine>>.Update
                .Set("Element.Slots", machine.Slots)
                .Set("Element.AmountInTransaction", machine.AmountInTransaction)
                .Set("Element.MoneyInside", machine.MoneyInside));

            return GetSnackMachine(machine.Id);
        }
    }
}
