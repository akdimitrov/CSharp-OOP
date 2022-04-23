using System;
using Chainblock.Contracts;

namespace Chainblock
{
    public class Transaction : ITransaction
    {
        public Transaction(int id, TransactionStatus status, string from, string to, double amount)
        {
            Id = id;
            Status = status;
            From = from;
            To = to;
            Amount = amount;
        }

        public int Id { get; set; }

        public TransactionStatus Status { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public double Amount { get; set; }

        public override bool Equals(object obj)
        {
            ITransaction transaction = obj as ITransaction;

            return this.Id == transaction.Id
                && this.Status == transaction.Status
                && this.From == transaction.From
                && this.To == transaction.To
                && this.Amount == transaction.Amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Status, this.From, this.To, this.Amount);
        }
    }
}
