namespace CommandPattern
{
    public class ProductCommand : ICommand
    {
        private readonly Product product;
        private readonly PriceAction priceAction;
        private readonly decimal amount;

        public ProductCommand(Product product, PriceAction priceAction, decimal amount)
        {
            this.product = product;
            this.priceAction = priceAction;
            this.amount = amount;
        }

        public void ExecuteAction()
        {
            if (this.priceAction == PriceAction.Increase)
            {
                this.product.IncreasePrice(amount);
            }
            else if (this.priceAction == PriceAction.Decrease)
            {
                this.product.DecreasePrice(amount);
            }
        }
    }
}
