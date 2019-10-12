using EvansSnackMachine.Logic.ValueObjects;

namespace EvansSnackMachine.Logic.Interfaces
{
    public interface ISnackMachine
    {
        void ReturnMoney();
        void BuySnack();
        void InsertMoney(Money money);

        Money MoneyInside { get; }
        Money MoneyInTransaction { get; }
    }
}
