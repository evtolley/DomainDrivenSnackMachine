using ATM.Logic.Entities;

namespace ATM.Logic.Interfaces
{
    public interface IATMRepository
    {
        AutomatedTellerMachine GetATM(string id);
        AutomatedTellerMachine CreateATM();
        AutomatedTellerMachine UpdateATM(AutomatedTellerMachine machine);
    }
}
