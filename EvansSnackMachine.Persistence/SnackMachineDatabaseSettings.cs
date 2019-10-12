namespace EvansSnackMachine.Persistence
{
    public class SnackMachineDatabaseSettings : ISnackMachineDatabaseSettings
    {
        public string SnackMachineCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
