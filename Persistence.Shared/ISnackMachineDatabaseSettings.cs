namespace Persistence.Shared
{
    public interface ISnackMachineDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string SnackMachineCollectionName { get; set; }
        string ATMCollectionName {get;set;}
        string SnackCollectionName { get; set; }
    }
}
