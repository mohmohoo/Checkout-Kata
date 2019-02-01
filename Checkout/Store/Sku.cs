using System;
using System.Text.RegularExpressions;

namespace Checkout.Store
{
    public class Sku : IDiscountableSku
    {
        public char Name { get; }

        public int UnitPrice { get; }

        public Sku(char name, int unitPrice)
        {
            if (name == '\0' || !Regex.IsMatch(name.ToString(), "[a-z]", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("Sku name is not a valid alphabet character", "name");
            }

            if (unitPrice <= 0)
            {
                throw new ArgumentException("Unit price is not valid value", "unitPrice");
            }

            Name = name;
            UnitPrice = unitPrice;
        }

        public int GetPrice(IItemCount items) => items.Value * UnitPrice;
    }
}
