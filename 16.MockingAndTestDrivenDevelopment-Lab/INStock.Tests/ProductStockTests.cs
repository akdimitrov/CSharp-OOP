namespace INStock.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using INStock.Contracts;
    using NUnit.Framework;

    public class ProductStockTests
    {
        private IProductStock productStock;
        private List<IProduct> products;
        private IProduct firstProduct;
        private IProduct notAddedProduct;
        private int productStockCount;

        [SetUp]
        public void SteUp()
        {
            productStock = new ProductStock();
            products = new List<IProduct>()
            {
                new Product("Orange Juice", 4.99m, 156),
                new Product("Cola", 1.2m, 99),
                new Product("Bread", 0.95m, 1),
                new Product("Salad", 3.25m, 16),
                new Product("Chips", 3.25m, 16)
            };

            foreach (var product in products)
            {
                productStock.Add(product);
            }

            productStockCount = productStock.Count;
            firstProduct = products[0];
            notAddedProduct = new Product("Beans", 2.99m, 80);
        }

        [Test]
        public void Test_ProductStock_Constructor()
        {
            IProductStock pStock = new ProductStock();
            Assert.AreEqual(0, pStock.Count);
        }

        [Test]
        public void Test_ProductStock_Add()
        {
            productStock.Add(notAddedProduct);
            Assert.AreEqual(productStockCount + 1, productStock.Count);
            Assert.That(productStock.Contains(notAddedProduct));
        }

        [Test]
        public void Test_ProductStock_Remove()
        {
            productStock.Remove(firstProduct);
            Assert.AreEqual(productStockCount - 1, productStock.Count);
            Assert.That(!productStock.Contains(firstProduct));
        }

        [Test]
        public void Test_ProductStock_Contains()
        {
            Assert.That(productStock.Contains(firstProduct));
            Assert.That(!productStock.Contains(notAddedProduct));
        }

        [Test]
        public void Test_ProductStock_Find_Positive()
        {
            var actualProduct = productStock[0];
            var wantedProduct = productStock.Find(0);
            Assert.That(wantedProduct.CompareTo(actualProduct) == 0);
            Assert.AreEqual(wantedProduct, actualProduct);
            Assert.AreSame(wantedProduct, actualProduct);
        }

        [Test]
        public void Test_ProductStock_Find_Negative()
        {
            Assert.Throws<IndexOutOfRangeException>(() => productStock.Find(-1));
            Assert.Throws<IndexOutOfRangeException>(() => productStock.Find(productStock.Count));
        }

        [Test]
        public void Test_ProductStock_Indexer_Positive()
        {
            var wanted1 = productStock[0];
            var wanted2 = productStock[1];
            var wanted3 = productStock[productStock.Count - 1];
            Assert.That(wanted1.CompareTo(products[0]) == 0);
            Assert.That(wanted2.CompareTo(products[1]) == 0);
            Assert.That(wanted3.CompareTo(products[productStock.Count - 1]) == 0);
        }

        [Test]
        public void Test_ProductStock_Indexer_Negative()
        {
            IProduct product;
            Assert.Throws<IndexOutOfRangeException>(() => product = productStock[-1]);
            Assert.Throws<IndexOutOfRangeException>(() => product = productStock[productStock.Count]);
        }

        [Test]
        public void Test_ProductStock_FindAllByPrice()
        {
            var foundProducts = productStock.FindAllByPrice(3.25m).ToList();
            Assert.AreEqual(2, foundProducts.Count);
            Assert.AreEqual(3.25m, foundProducts[0].Price);
            Assert.AreEqual(3.25m, foundProducts[1].Price);

            foundProducts = productStock.FindAllByPrice(9.86m).ToList();
            Assert.AreEqual(0, foundProducts.Count);
        }

        [Test]
        public void Test_ProductStock_FindAllByQuantity()
        {
            var foundProducts = productStock.FindAllByQuantity(16).ToList();
            Assert.AreEqual(2, foundProducts.Count);
            Assert.AreEqual(16, foundProducts[0].Quantity);
            Assert.AreEqual(16, foundProducts[1].Quantity);

            foundProducts = productStock.FindAllByQuantity(2).ToList();
            Assert.AreEqual(0, foundProducts.Count);
        }

        [Test]
        public void Test_ProductStock_FindAllInRange()
        {
            var foundProducts = productStock.FindAllInRange(1.2m, 3.25m).ToList();
            Assert.AreEqual(3, foundProducts.Count);
            for (int i = 0; i < foundProducts.Count; i++)
            {
                Assert.That(foundProducts[i].Price >= 1.2m && foundProducts[i].Price <= 3.25m);
            }

            foundProducts = productStock.FindAllInRange(3.5m, 4.5m).ToList();
            Assert.AreEqual(0, foundProducts.Count);
        }

        [Test]
        public void Test_ProductStock_FindByLabel_Positive()
        {
            var wanted = productStock.FindByLabel("Cola");
            Assert.That(wanted.Label == "Cola");
        }

        [Test]
        public void Test_ProductStock_FindByLabel_Negative()
        {
            IProduct wanted;
            Assert.Throws<ArgumentException>(() => wanted = productStock.FindByLabel("Whiskey"));
        }

        [Test]
        public void Test_ProductStock_FindMostExpensiveProduct()
        {
            var maxPrice = products.Max(x => x.Price);
            var product = productStock.FindMostExpensiveProduct();
            Assert.IsNotNull(product);
            Assert.AreEqual(maxPrice, product.Price);
        }

        [Test]
        public void Test_ProductStock_GetEnumerator()
        {
            for (int i = 0; i < products.Count; i++)
            {
                Assert.AreEqual(productStock[i], products[i]);
            }
        }
    }
}
