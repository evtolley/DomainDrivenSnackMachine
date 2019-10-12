using EvansSnackMachine.Logic.Entities;
using EvansSnackMachine.Logic.Factories;
using EvansSnackMachine.Logic.Interfaces;
using EvansSnackMachine.Persistence.Entities;
using MongoDB.Driver;
using System;

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
            var snackMachineDbo = new SnackMachineDBO();
            _snackMachines.InsertOne(snackMachineDbo);

            return SnackMachineFactory.Build(snackMachineDbo);
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
            _snackMachines.UpdateOne(Builders<SnackMachineDBO>.Filter.Eq("Id", machine.Id), Builders<SnackMachineDBO>.Update.Set("MoneyInTransaction", dbo.MoneyInTransaction));

            return GetSnackMachine(machine.Id);
        }
    }
}
