﻿using EvansSnackMachine.Logic.Interfaces;
using EvansSnackMachine.Logic.ValueObjects;
using System;
using System.Linq;

namespace EvansSnackMachine.Logic.Entities
{
    public sealed class SnackMachine : Entity, ISnackMachine
    {
        public Money MoneyInside { get; private set; }
        public Money MoneyInTransaction { get; private set; }

        public SnackMachine()
        {
            MoneyInside = Money.None;
            MoneyInTransaction = Money.None;
        }

        public SnackMachine(string id, Money moneyInside, Money moneyInTransaction) : base(id)
        {
            MoneyInside = moneyInside;
            MoneyInTransaction = moneyInTransaction;
        }
       
        public void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = { Money.OneCent, Money.TenCent, Money.Quarter, Money.Dollar, Money.FiveDollar, Money.TwentyDollar };

            if (!coinsAndNotes.Contains(money))
            {
                throw new InvalidOperationException();
            }

            this.MoneyInTransaction += money;
        }

        public void ReturnMoney()
        {
            this.MoneyInTransaction = Money.None;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = Money.None;
        }
    }
}