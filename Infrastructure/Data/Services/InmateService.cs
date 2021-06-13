using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.CoreDtos;
using Core.DataFilters;
using Core.Entities;
using Core.Enumerations;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Data.Services
{
    public class InmateService : IInmateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;

        public InmateService(IUnitOfWork unitOfWork, IMapper mapper , ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _transactionService = transactionService;
        }
        public async Task<IReadOnlyList<InmateToReturnDto>> GetInmates(InmateFilter filter)
        {
            var spec = new InmateSpecification(filter);
            return await  _unitOfWork.Repository<Inmate>().FindAllBySpecAsync<InmateToReturnDto>(spec);
        }

        public async Task<int> GetInmatesCount(InmateFilter filter)
        {
            var spec = new InmateSpecification(filter,true);
            return await _unitOfWork.Repository<Inmate>().GetCountForSpecAsync(spec);
        }

        public async Task<Inmate> GetInmate(int id)
        {
            return await _unitOfWork.Repository<Inmate>().FindByIdAsync(id);
        }

        public async Task<Inmate> AddInmate(Inmate inmate)
        {
            var inmateToReturn = _unitOfWork.Repository<Inmate>().Add(inmate);
            await _unitOfWork.Complete();
            return inmateToReturn;
        }
        public async Task<bool> UpdateInmate(Inmate inmateRequest)
        {
            var inmate = await  _unitOfWork.Repository<Inmate>().FindByIdAsync(inmateRequest.Id);
            if (inmate == null) return false;

            _mapper.Map(inmateRequest, inmate);
            
            return await _unitOfWork.Complete() > 0;
        }

        public async Task<bool> UpdateInmatePhoto(string PhotoUrl, int inmateId)
        {
            var inmate = await _unitOfWork.Repository<Inmate>().FindByIdAsync(inmateId);
            if (inmate == null) return false;
            inmate.PictureUrl = PhotoUrl;
            return await _unitOfWork.Complete() > 0;
        }

        public async Task<bool> DeactivateInmate(DeactivatedInmate deactivatedInmate)
        {
            var inmate = await _unitOfWork.Repository<Inmate>().FindByIdAsync(deactivatedInmate.InmateId);
            
            inmate.AmountDue -= deactivatedInmate.Amount;

            if(inmate.AmountDue < 0)
            {
                inmate.Savings += inmate.AmountDue;
                inmate.AmountDue = 0;                
            }

            if(deactivatedInmate.IsSettlement && inmate.AmountDue == 0)
            {
                var associatedVendorNamesList = new List<string> { VendorNames.Main, VendorNames.BillPaymentAccount };

                var vendorSpec = new VendorSpecification(associatedVendorNamesList);

                var vendors = await _unitOfWork.Repository<Vendor>().FindAllBySpecAsync(vendorSpec);

                var mainAccount = vendors.FirstOrDefault(v => v.Name == VendorNames.Main);

                var billPaymentAccount = vendors.FirstOrDefault(v => v.Name == VendorNames.BillPaymentAccount);

                inmate.Status = InmateStatus.Left;
              
                _unitOfWork.Repository<DeactivatedInmate>().Add(deactivatedInmate);

                var settlementTxn = new TransactionDetail
                {
                    Amount = inmate.Savings,
                    Name = "Settlement - " + inmate.FullName,
                    TransactionDate = DateTime.Now,
                    PaidPartyId = mainAccount.Id,
                    PaidToId = billPaymentAccount.Id,
                    CategoryId = 6
                };

                inmate.Savings = 0;

                await _transactionService.PostTransaction(settlementTxn);
            }

            var lastDate = deactivatedInmate.LastDate;

            if(lastDate > DateTime.Now) {

                var lastDayOfMonth = DateTime.DaysInMonth(lastDate.Year, lastDate.Month);

                var leaveLastDate = new DateTime(lastDate.Year, lastDate.Month, lastDayOfMonth);

                var leave = new Leave
                {
                    InmateId = inmate.Id,
                    FromDate = lastDate,
                    ToDate = leaveLastDate,
                    LeaveReason = "Inmate Left",
                    Status = LeaveStatus.Approved
                };

                _unitOfWork.Repository<Leave>().Add(leave);              

            }

            await _unitOfWork.Complete();
            return true ;
        
        }

        
    }
}