using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INStock.Contracts;

namespace INStock
{
    public class ProductStock : IProductStock
    {
        private IList<IProduct> productStock;

        public ProductStock()
        {
            productStock = new List<IProduct>();
        }

        public IProduct this[int index]
        {
            get
            {
                EnsureIsInRage(index);
                return productStock[index];
            }
            set
            {
                EnsureIsInRage(index);
                productStock[index] = value;
            }
        }

        public int Count => productStock.Count();

        public void Add(IProduct product)
        {

            productStock.Add(product);
        }

        public bool Contains(IProduct product)
        {
            return productStock.Any(p => p == product);
        }

        public IProduct Find(int index)
        {
            EnsureIsInRage(index);
            return productStock[index];
        }

        public IEnumerable<IProduct> FindAllByPrice(double price)
        {
            return productStock.Where(p => p.Price == (decimal)price);
        }

        public IEnumerable<IProduct> FindAllByQuantity(int quantity)
        {
            return productStock.Where(p => p.Quantity == quantity);
        }

        public IEnumerable<IProduct> FindAllInRange(double lo, double hi)
        {
            return productStock.Where(p => p.Price >= (decimal)lo && p.Price <= (decimal)hi).OrderByDescending(p => p.Price);
        }

        public IProduct FindByLabel(string label)
        {
            var product = productStock.FirstOrDefault(p => p.Label == label);
            if (product is null)
            {
                throw new ArgumentException();
            }

            return product;
        }

        public IProduct FindMostExpensiveProduct()
        {
            return productStock.OrderByDescending(p => p.Price).FirstOrDefault();
        }

        public bool Remove(IProduct product)
        {
            return productStock.Remove(product);
        }

        public IEnumerator<IProduct> GetEnumerator()
        {
            foreach (var product in productStock)
            {
                yield return product;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void EnsureIsInRage(int index)
        {
            if (index < 0 || index >= productStock.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
