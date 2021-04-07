using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataFilters;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITransactionService
    {
        Task<IReadOnlyList<Transaction>> GetTransactions(TransactionsFilter specParams);
        Task<Transaction> GetTransaction(int id);
        Task<bool> PostTransaction(Transaction transaction);
    }
}