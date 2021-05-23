using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
    public class CheckOut:ICheckOut
    {
        private char[] _itemList;

        private readonly IEnumerable<Product> _products;

        public CheckOut(IEnumerable<Product> products)
        {
            _products = products;
            _itemList = new char[] { };
        }

        public ICheckOut Scan(String scanItem)
        {
            if (!String.IsNullOrWhiteSpace(scanItem))
            {
                _itemList = scanItem.ToCharArray().Where(sku => _products.Any(product => product.SKU == sku))
                    .ToArray();
            }

            return this;
        }


        public int GetTotalPrice()
        {
            var totalPrice = 0;
            if (_itemList.Any())
            {
                foreach (var item in _itemList)
                {
                    totalPrice = _products.Single(p => p.SKU == item).Price;
                }
            }
            return totalPrice;

        }




    }
}
