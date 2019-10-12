namespace EvansSnackMachine.Persistence
{
    public interface ISnackMachineDatabaseSettings
    {
        string SnackMachineCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
