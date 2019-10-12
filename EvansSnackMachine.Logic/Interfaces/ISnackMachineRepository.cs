using EvansSnackMachine.Logic.Entities;

namespace EvansSnackMachine.Logic.Interfaces
{
    public interface ISnackMachineRepository
    {
        SnackMachine GetSnackMachine(string id);
        SnackMachine CreateSnackMachine();
        SnackMachine UpdateSnackMachine(SnackMachine snackMachine);
    }
}
