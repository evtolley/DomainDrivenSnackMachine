using EvansSnackMachine.Logic.Exceptions;
using EvansSnackMachine.Logic.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvansSnackMachine.Logic.Entities
{
    public sealed class SnackMachine : AggregateRoot
    {
        public Money MoneyInside { get; private set; }
        public decimal AmountInTransaction { get; private set; }
        private IList<Slot> Slots { get; }

        public SnackMachine()
        {
            MoneyInside = Money.None;
            AmountInTransaction = 0;

            //initializing empty slots
            Slots = new List<Slot>()
            {
                new Slot(this, null, 1),
                new Slot(this, null, 2),
                new Slot(this, null, 3),
            };
        }

        public SnackMachine(string id, Money moneyInside, decimal amountInTransaction) : base(id)
        {
            MoneyInside = moneyInside;
            AmountInTransaction = amountInTransaction;

            //TO DO: Make a constructor for factory
            //Slots = new List<Slot>()
            //{
            //    new Slot(this, null, 0, 0m, 1)
            //}
        }

        public void LoadMoney(Money money)
        {
            this.MoneyInside += money;
        }
       
        public void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = { Money.OneCent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar };

            if (!coinsAndNotes.Contains(money))
            {
                throw new InvalidOperationException();
            }

            this.AmountInTransaction += money.Amount;
            this.MoneyInside += money;
        }

        public void ReturnMoney()
        {
            Money moneyToReturn = MoneyInside.AllocateToReturn(AmountInTransaction);
            MoneyInside -= moneyToReturn;

            this.AmountInTransaction = 0;
        }

        public void BuySnack(int position)
        {
            // Don't allow purchase if not enough money inserted
            if(GetSlot(position).SnackPile.Price > this.AmountInTransaction)
            {
                throw new InvalidOperationException();
            }

            Slot slot = GetSlot(position);
            slot.SnackPile = slot.SnackPile.SubtractOne();

            //find the amount of change to return 
            Money change = MoneyInside.AllocateToReturn(AmountInTransaction - slot.SnackPile.Price);

            if(change.Amount < AmountInTransaction - slot.SnackPile.Price)
            {
                throw new CannotMakeChangeException();
            }

            MoneyInside -= change;
            AmountInTransaction = 0;
        }

        public void LoadSnacks(int position, SnackPile snackPile)
        {
            Slot slot = GetSlot(position);
            slot.SnackPile = snackPile;
        }

        public SnackPile GetSnackPile(int position)
        {
            return GetSlot(position).SnackPile;
        }

        private Slot GetSlot(int position)
        {
            return this.Slots.Single(x => x.Position == position);
        }
    }
}
