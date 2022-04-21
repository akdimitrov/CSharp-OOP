namespace INStock.Tests
{
    using NUnit.Framework;

    public class ProductTests
    {
        [Test]
        public void Test_Product_Constructor()
        {
            Product product = new Product("aa", 29.99m, 5);

            Assert.AreEqual("aa", product.Label);
            Assert.AreEqual(29.99m, product.Price);
            Assert.AreEqual(5, product.Quantity);
        }

        [Test]
        public void Test_Product_CompareTo()
        {
            Product product1 = new Product("aa", 29.99m, 5);
            Product product2 = new Product("bb", 29.99m, 5);
            Product product3 = new Product("aa", 16.30m, 1);

            Assert.That(product1.CompareTo(product3) == 0);
            Assert.That(product1.CompareTo(product2) != 0);
        }
    }
}