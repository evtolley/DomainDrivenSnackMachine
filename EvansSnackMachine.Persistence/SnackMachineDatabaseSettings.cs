namespace EvansSnackMachine.Persistence
{
    public class SnackMachineDatabaseSettings : ISnackMachineDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string SnackCollectionName { get; set; }
        public string SnackMachineCollectionName { get; set; }
    }
}
