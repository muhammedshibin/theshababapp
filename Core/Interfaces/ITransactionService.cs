using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CoreDtos;
using Core.DataFilters;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITransactionService
    {
        Task<IReadOnlyList<TransactionDto>> GetTransactions(TransactionsFilter specParams);
        Task<int> GetTransactionsCount(TransactionsFilter specParams);
        Task<IReadOnlyList<Category>> GetTransactionCategories();
        Task<TransactionDetail> GetTransaction(int id);
        Task<bool> PostTransaction(TransactionDetail transaction);
        Task<bool> UpdateTransaction(TransactionDetail updated);
        Task<bool> DeleteTransaction(int id);
        Task<bool> UpdateTransactionCategories(List<Category> categories);
    }
}