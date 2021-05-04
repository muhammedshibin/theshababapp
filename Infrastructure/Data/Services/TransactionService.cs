using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataFilters;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Data.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IReadOnlyList<TransactionDetail>> GetTransactions(TransactionsFilter specParams)
        {
            var spec = new TransactionWithCategoryAndVendorSpecification(specParams);
            var transactions = await  _unitOfWork.Repository<TransactionDetail>().FindAllBySpecAsync(spec);
            return transactions;
        }

        public async Task<TransactionDetail> GetTransaction(int id)
        {
            var spec = new TransactionWithCategoryAndVendorSpecification(id);
            var transaction = await _unitOfWork.Repository<TransactionDetail>().FindOneBySpecAsync(spec);
            return transaction;
        }

        
        public async Task<bool> PostTransaction(TransactionDetail transaction)
        {
           _unitOfWork.Repository<TransactionDetail>().Add(transaction);

            var vendor = await _unitOfWork.Repository<Vendor>().FindByIdAsync(transaction.PaidPartyId);

            if (vendor != null)
            {
                vendor.AmountInHand = transaction.IsExpense
                    ? vendor.AmountInHand - transaction.Amount
                    : vendor.AmountInHand + transaction.Amount;
                vendor.ModfiedOn = DateTime.Now;
            }
            
            return await _unitOfWork.Complete() > 0;
        }
    }
}