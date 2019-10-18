using ATM.Logic.Entities;
using ATM.Logic.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Shared;

namespace ATM.Persistence
{
    public class ATMRepository : IATMRepository
    {
        private readonly IMongoCollection<MongoObject<AutomatedTellerMachine>> _atms;

        public ATMRepository(ISnackMachineDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _atms = database.GetCollection<MongoObject<AutomatedTellerMachine>>(settings.ATMCollectionName);
        }
        public AutomatedTellerMachine CreateATM()
        {
            var atm = new MongoObject<AutomatedTellerMachine>(new AutomatedTellerMachine());
            _atms.InsertOne(atm);

            return atm.Unwrap();
        }

        public AutomatedTellerMachine GetATM(string id)
        {
            return _atms.Find(Builders<MongoObject<AutomatedTellerMachine>>.Filter.Eq("_id", ObjectId.Parse(id)))
                .FirstOrDefault()
                .Unwrap();
        }

        public AutomatedTellerMachine UpdateATM(AutomatedTellerMachine machine)
        {
            _atms.UpdateOne(Builders<MongoObject<AutomatedTellerMachine>>
               .Filter.Eq("_id", ObjectId.Parse(machine.Id)),
               Builders<MongoObject<AutomatedTellerMachine>>.Update
               .Set("Element.MoneyCharged", machine.MoneyCharged)
               .Set("Element.MoneyInside", machine.MoneyInside));

               return GetATM(machine.Id);
        }
    }
}
