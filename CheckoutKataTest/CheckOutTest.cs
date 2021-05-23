using System;
using System.Collections.Generic;
using System.Configuration;
using CheckoutKata;
using NUnit.Framework;


namespace CheckoutKataTest
{
  
    [TestFixture]
    public class CheckOutTest
    {

        private  ICheckOut _checkout;
        [SetUp]
       public void Setup()
        {
            var products = new List<Product>()
            {
                new Product() {SKU = 'A', Price = 50},
                new Product() {SKU = 'B', Price = 30},
                new Product() {SKU = 'C', Price = 20}
            };
            _checkout = new CheckOut(products);
        }

        

        [Test]
        public void When_no_items_are_scanned_throw_Exception()
        {
          var result=  _checkout.Scan("").GetTotalPrice();
          Assert.AreEqual(0 , result);
        }

        [Test]
        [TestCase("A",50)]
        [TestCase("B", 30)]
        public void When_single_item_scanned_return_correct_price(string product, int expectedPrice)
        { 
            
            var totalPrice = _checkout.Scan(product).GetTotalPrice();
            Assert.AreEqual(totalPrice,expectedPrice);

        }






    }
}
