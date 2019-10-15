using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.Factories;
using EvansSnackMachine.Logic.Interfaces;
using EvansSnackMachine.Persistence.Models;
using MongoDB.Driver;

namespace EvansSnackMachine.Persistence.Repositories
{

    public class SnackMachineRepository : ISnackMachineRepository
    {
        private readonly IMongoCollection<SnackMachineDBO> _snackMachines;

        public SnackMachineRepository(ISnackMachineDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _snackMachines = database.GetCollection<SnackMachineDBO>(settings.SnackMachineCollectionName);
        }

        public SnackMachine CreateSnackMachine()
        {
            var snackMachine = new SnackMachine();
            var dbo = new SnackMachineDBO(snackMachine);
            _snackMachines.InsertOne(dbo);

            return SnackMachineFactory.Build(dbo);
        }

        public SnackMachine GetSnackMachine(string id)
        {
            return SnackMachineFactory.Build(
                _snackMachines.Find(machine => machine.Id == id).FirstOrDefault()
                );
        }

        public SnackMachine UpdateSnackMachine(SnackMachine machine)
        {
            var dbo = new SnackMachineDBO(machine);
            _snackMachines.UpdateOne(Builders<SnackMachineDBO>.Filter.Eq("Id", machine.Id), Builders<SnackMachineDBO>.Update.Set("MoneyInside", dbo.MoneyInside));
            _snackMachines.UpdateOne(Builders<SnackMachineDBO>.Filter.Eq("Id", machine.Id), Builders<SnackMachineDBO>.Update.Set("AmountInTransaction", dbo.AmountInTransaction));
            _snackMachines.UpdateOne(Builders<SnackMachineDBO>.Filter.Eq("Id", machine.Id), Builders<SnackMachineDBO>.Update.Set("Slots", dbo.Slots));

            return GetSnackMachine(machine.Id);
        }
    }
}
