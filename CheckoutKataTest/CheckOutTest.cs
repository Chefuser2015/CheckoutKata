using System.Collections.Generic;
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
                new Product() {Sku = 'A', Price = 50},
                new Product() {Sku = 'B', Price = 30},
                new Product() {Sku = 'C', Price = 20},
                new Product() {Sku = 'D', Price = 15}
            };
            IEnumerable<Discount> discounts = new[]
            {
                new Discount{Sku = 'A', Quantity = 3, Value = 20},
                new Discount{Sku = 'B', Quantity = 2, Value = 15}
            };
            _checkout = new CheckOut(products, discounts);
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
        [TestCase("C", 20)]
        [TestCase("D", 15)]

        public void When_single_item_scanned_return_correct_price(string product, int expectedPrice)
        { 
            
            var totalPrice = _checkout.Scan(product).GetTotalPrice();
            Assert.AreEqual(totalPrice,expectedPrice);

        }
        [Test]
        [TestCase("AC", 70)]
        [TestCase("BA", 80)]
        [TestCase("CD", 35)]
        [TestCase("ABC", 100)]
        [TestCase("CBD", 65)]
        [TestCase("ABCC", 120)]
        [TestCase("DCBA", 115)]

        public void Scan_no_discount_combinations_and_expect_total(string scan, int expected)
        {
            Assert.AreEqual(expected, _checkout.Scan(scan).GetTotalPrice());
        }

        [Test]
        [TestCase("AAA", 130)]
        [TestCase("AAAB", 160)]
        [TestCase("AAABB", 175)]
        [TestCase("AAAAAA", 260)]
        [TestCase("AAAAAABB", 305)]
        [TestCase("BB", 45)]
        [TestCase("ABB", 95)]
        [TestCase("BBBB", 90)]
        [TestCase("BBBBAC", 160)]
        public void Scan_discounted_combinations_and_expect_correct_total(string scan, int expected)
        {
            Assert.AreEqual(expected, _checkout.Scan(scan).GetTotalPrice());
        }


        [Test]
        [TestCase("XYZKL", "")]
        [TestCase("CDXY", "CD")]
        [TestCase("ABCD", "ABCD")]
        [TestCase("AXBYCZD", "ABCD")]
        public void Scan_should_only_process_existing_product(string scan, string expected)
        {
            Assert.AreEqual(
                expected,
                (_checkout.Scan(scan).ScannedProducts)
            );
        }






    }
}
