using System.Diagnostics.CodeAnalysis;
using INStock.Contracts;

namespace INStock
{
    public class Product : IProduct
    {
        public Product(string label, decimal price, int quantity)
        {
            Label = label;
            Price = price;
            Quantity = quantity;
        }

        public string Label { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        public int CompareTo([AllowNull] IProduct other)
        {
            return this.Label.CompareTo(other.Label);
        }
    }
}
