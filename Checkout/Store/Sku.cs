using System;
using System.Text.RegularExpressions;

namespace Checkout.Store
{
    public class Sku : ISku
    {
        private readonly int _unitPrice;

        public char Name { get; }

        public Sku(char name, int unitPrice)
        {
            if (Regex.IsMatch(name.ToString(), "[a-z]", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("Sku name is not a valid alphabet character", "name");
            }

            if (unitPrice <= 0)
            {
                throw new ArgumentException("Unit price is not valid value", "unitPrice");
            }

            Name = name;
            _unitPrice = unitPrice;
        }

        public int GetPrice(IItemCount items) => items.Value * _unitPrice;
    }
}
