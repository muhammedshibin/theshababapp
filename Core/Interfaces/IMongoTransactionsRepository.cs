using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMongoTransactionsRepository
    {
        Task Delete(int Id);
        Task<IReadOnlyList<TransactionDetail>> GetAll();
        Task<TransactionDetail> GetEntityById(int Id);
        Task<TransactionDetail> Save(TransactionDetail transaction);
        Task<List<TransactionDetail>> SaveMany(List<TransactionDetail> transactionDetails);
        Task Update(int Id, TransactionDetail transaction);
    }
}