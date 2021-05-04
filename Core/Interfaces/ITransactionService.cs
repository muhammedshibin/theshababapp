using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataFilters;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITransactionService
    {
        Task<IReadOnlyList<TransactionDetail>> GetTransactions(TransactionsFilter specParams);
        Task<TransactionDetail> GetTransaction(int id);
        Task<bool> PostTransaction(TransactionDetail transaction);
    }
}