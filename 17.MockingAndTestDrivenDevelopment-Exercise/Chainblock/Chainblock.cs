using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chainblock.Contracts;

namespace Chainblock
{
    public class Chainblock : IChainblock
    {
        private readonly Dictionary<int, ITransaction> transactions;

        public Chainblock()
        {
            this.transactions = new Dictionary<int, ITransaction>();
        }

        public int Count => this.transactions.Count;

        public void Add(ITransaction tx)
        {
            if (!this.transactions.ContainsKey(tx.Id))
            {
                this.transactions[tx.Id] = tx;
            }
        }

        public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
        {
            if (!this.transactions.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            this.transactions[id].Status = newStatus;
        }

        public bool Contains(ITransaction tx) => this.transactions.ContainsKey(tx.Id);

        public bool Contains(int id) => this.transactions.ContainsKey(id);

        public IEnumerable<ITransaction> GetAllInAmountRange(double lo, double hi)
        {
            var result = this.transactions
                .Values
                .Where(x => x.Amount >= lo && x.Amount <= hi)
                .ToArray();

            return result;
        }

        public IEnumerable<ITransaction> GetAllOrderedByAmountDescendingThenById()
        {
            var result = this.transactions.Values
               .OrderByDescending(x => x.Amount)
               .ThenBy(x => x.Id)
               .ToArray();

            return result;
        }

        public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
        {
            var result = this.transactions
                .Where(x => x.Value.Status == status)
                .OrderByDescending(x => x.Value.Amount)
                .Select(x => x.Value.To)
                .ToArray();

            return result.Any() ? result : throw new InvalidOperationException();
        }

        public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
        {
            var result = this.transactions
                .Where(x => x.Value.Status == status)
                .OrderByDescending(x => x.Value.Amount)
                .Select(x => x.Value.From)
                .ToArray();

            return result.Any() ? result : throw new InvalidOperationException();
        }

        public ITransaction GetById(int id)
        {
            return this.transactions.ContainsKey(id)
                ? this.transactions[id]
                : throw new InvalidOperationException();
        }

        public IEnumerable<ITransaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
        {
            var result = this.transactions
                .Values
                .Where(x => x.To == receiver && x.Amount >= lo && x.Amount < hi)
                .OrderByDescending(x => x.Amount)
                .ThenBy(x => x.Id)
                .ToArray();

            return result.Any() ? result : throw new InvalidOperationException();
        }

        public IEnumerable<ITransaction> GetByReceiverOrderedByAmountThenById(string receiver)
        {
            var result = this.transactions
                .Values
                .Where(x => x.To == receiver)
                .OrderByDescending(x => x.Amount)
                .ThenBy(x => x.Id)
                .ToArray();

            return result.Any() ? result : throw new InvalidOperationException();
        }

        public IEnumerable<ITransaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
        {
            var result = this.transactions
                .Values
                .Where(x => x.From == sender && x.Amount > amount)
                .OrderByDescending(x => x.Amount)
                .ToArray();

            return result.Any() ? result : throw new InvalidOperationException();
        }

        public IEnumerable<ITransaction> GetBySenderOrderedByAmountDescending(string sender)
        {
            var result = this.transactions
                .Values
                .Where(x => x.From == sender)
                .OrderByDescending(x => x.Amount)
                .ToArray();

            return result.Any() ? result : throw new InvalidOperationException();
        }

        public IEnumerable<ITransaction> GetByTransactionStatus(TransactionStatus status)
        {
            var result = this.transactions
                .Values
                .Where(x => x.Status == status)
                .OrderByDescending(x => x.Amount)
                .ToArray();

            return result.Any() ? result : throw new InvalidOperationException();
        }

        public IEnumerable<ITransaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
        {
            var result = this.transactions
                .Values
                .Where(x => x.Status == status && x.Amount <= amount)
                .OrderByDescending(x => x.Amount)
                .ToArray();

            return result;
        }

        public void RemoveTransactionById(int id)
        {
            if (!this.transactions.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }

            this.transactions.Remove(id);
        }

        public IEnumerator<ITransaction> GetEnumerator()
        {
            foreach (var transaction in this.transactions)
            {
                yield return transaction.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
