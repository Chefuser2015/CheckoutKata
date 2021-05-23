using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class CheckOut:ICheckOut
    {
        private char[] _scannedItems;

        private readonly IEnumerable<Product> _products;
        private readonly IEnumerable<Discount> _discounts;
        public string ScannedProducts => new string(_scannedItems);

        public CheckOut(IEnumerable<Product> products, IEnumerable<Discount> discounts)
        {
            _products = products;
            _discounts = discounts;
            _scannedItems = new char[] { };
        }

        public ICheckOut Scan(string scanItem)
        {
            if (!string.IsNullOrWhiteSpace(scanItem))
            {
                _scannedItems = scanItem.ToCharArray().Where(sku => _products.Any(product => product.Sku == sku))
                    .ToArray();
            }

            return this;
        }


        public int GetTotalPrice()
        {
            var totalPrice = 0;
            if (!_scannedItems.Any()) return totalPrice;
            totalPrice = _scannedItems.ToArray().Sum(CalculatePrice);
            var totalDiscount = _discounts.Sum(discount => CalculateDiscount(discount, _scannedItems));
            totalPrice -= totalDiscount;
            return totalPrice;

        }
        private int CalculatePrice(char sku)
        {
            return _products.SingleOrDefault(p => p.Sku == sku).Price;
        }

        private int CalculateDiscount(Discount discount, char[] scannedProduct)
        {
            var itemCount = scannedProduct.Count(item => item == discount.Sku);
            return (itemCount / discount.Quantity) * discount.Value;
        }



    }
}
