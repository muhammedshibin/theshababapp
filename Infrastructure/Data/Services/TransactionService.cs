using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.CoreDtos;
using Core.DataFilters;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Data.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TransactionDto>> GetTransactions(TransactionsFilter specParams)
        {
            var spec = new TransactionWithCategoryAndVendorSpecification(specParams);
            var transactions = await _unitOfWork.Repository<TransactionDetail>()
                .FindAllBySpecAsync<TransactionDto>(spec);
            return transactions;
        }
        public async Task<int> GetTransactionsCount(TransactionsFilter specParams)
        {
            var spec = new TransactionWithCategoryAndVendorSpecification(specParams, true);
            return await _unitOfWork.Repository<TransactionDetail>().GetCountForSpecAsync(spec);
        }
        public async Task<TransactionDetail> GetTransaction(int id)
        {
            var spec = new TransactionWithCategoryAndVendorSpecification(id);
            var transaction = await _unitOfWork.Repository<TransactionDetail>().FindOneBySpecAsync(spec);
            return transaction;
        }

        public async Task<IReadOnlyList<Category>> GetTransactionCategories()
        {
            return await _unitOfWork.Repository<Category>().FindAllAsync();
        }

        public async Task<bool> PostTransaction(TransactionDetail transaction)
        {
            _unitOfWork.Repository<TransactionDetail>().Add(transaction);

            var vendorIds = new List<int>();
            vendorIds.Add(transaction.PaidPartyId);
            vendorIds.Add(transaction.PaidToId);

            var vendors = await _unitOfWork.Repository<Vendor>().FindAllBySpecAsync(new VendorSpecification(vendorIds));

            foreach (var vendor in vendors)
            {
                if (vendor.Id == transaction.PaidPartyId)
                {
                    vendor.AmountInHand = transaction.IsExpense
                       ? vendor.AmountInHand - transaction.Amount
                       : vendor.AmountInHand + transaction.Amount;

                }
                if (vendor.Id == transaction.PaidToId)
                {
                    vendor.AmountInHand = transaction.IsExpense
                       ? vendor.AmountInHand + transaction.Amount
                       : vendor.AmountInHand - transaction.Amount;
                }
                vendor.ModfiedOn = DateTime.Now;
            }            

            return await _unitOfWork.Complete() > 0;
        }

        public async Task<bool> UpdateTransaction(TransactionDetail updated)
        {

            var existing = await  _unitOfWork.Repository<TransactionDetail>()
                .FindOneBySpecAsync(new TransactionWithCategoryAndVendorSpecification(updated.Id));

            var vendorIds = new List<int>
            {
                updated.PaidPartyId,
                updated.PaidToId,
                existing.PaidPartyId,
                existing.PaidToId
            };

            var vendors = await _unitOfWork.Repository<Vendor>()
                .FindAllBySpecAsync(new VendorSpecification(vendorIds.Distinct().ToList()));


            if (existing.PaidPartyId == updated.PaidPartyId)
            {
                var existingPaidPartyVendor = vendors.FirstOrDefault(v => v.Id == existing.PaidPartyId);
                existingPaidPartyVendor.AmountInHand = existingPaidPartyVendor.AmountInHand + existing.Amount - updated.Amount;
            }

            if (existing.PaidPartyId != updated.PaidPartyId)
            {
                var existingPaidPartyVendor = vendors.FirstOrDefault(v => v.Id == existing.PaidPartyId);
                existingPaidPartyVendor.AmountInHand += existing.Amount;

                var newPaidPartyVendor = vendors.FirstOrDefault(v => v.Id == updated.PaidPartyId);
                newPaidPartyVendor.AmountInHand -= updated.Amount;
            }

            if (existing.PaidToId == updated.PaidToId)
            {
                var existingPaidToVendor = vendors.FirstOrDefault(v => v.Id == existing.PaidToId);
                existingPaidToVendor.AmountInHand = existingPaidToVendor.AmountInHand - existing.Amount + updated.Amount;
            }

            if (existing.PaidToId != updated.PaidToId)
            {
                var existingPaidToVendor = vendors.FirstOrDefault(v => v.Id == existing.PaidToId);
                existingPaidToVendor.AmountInHand -= existing.Amount;

                var newPaidPartyToVendor = vendors.FirstOrDefault(v => v.Id == updated.PaidPartyId);
                newPaidPartyToVendor.AmountInHand += updated.Amount;
            }

            existing = _mapper.Map(updated, existing);

            _unitOfWork.Repository<TransactionDetail>().Update(existing);

            return await _unitOfWork.Complete() > 0;
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            var transaction = await  _unitOfWork.Repository<TransactionDetail>().FindByIdAsync(id);

            if (transaction == null) throw new KeyNotFoundException($"Transaction with Id {id} does not exist");

            _unitOfWork.Repository<TransactionDetail>().Remove(transaction);

            return await _unitOfWork.Complete() > 0;
            
        }
    }
}