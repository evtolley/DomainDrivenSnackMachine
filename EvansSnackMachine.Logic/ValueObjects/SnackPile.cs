using EvansSnackMachine.Logic.Entities;
using System;

namespace EvansSnackMachine.Logic.ValueObjects
{
    public sealed class SnackPile : ValueObject<SnackPile>
    {
        public int Quantity { get; }
        public decimal Price { get; }
        public Snack Snack { get; }

        public SnackPile(Snack snack, int quantity, decimal price)
        {
            if(quantity < 0 || price < 0)
            {
                throw new InvalidOperationException();
            }

            //checking to make sure price is a valid cent rounded value
            if(price % .01m > 0)
            {
                throw new InvalidOperationException();
            }

            this.Snack = snack;
            this.Quantity = quantity;
            this.Price = price;
        }

        public SnackPile SubtractOne()
        {
            return new SnackPile(this.Snack, this.Quantity - 1, this.Price);
        }

        protected override bool EqualsCore(SnackPile other)
        {
            return this.Snack == other.Snack
                && this.Quantity == other.Quantity
                && this.Price == other.Price;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Snack.GetHashCode();
                hashCode = (hashCode * 397) ^ Quantity;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();

                return hashCode;
            }
        }
    }
}
