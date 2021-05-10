using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CoreDtos;
using Core.DataFilters;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Data.Services
{
    public class InmateService : IInmateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InmateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<bool> AddInmate(Inmate inmate)
        {
            _unitOfWork.Repository<Inmate>().Add(inmate);
            return await _unitOfWork.Complete() > 0;
        }
        public async Task<bool> UpdateInmate(Inmate inmateRequest)
        {
            var inmate = await  _unitOfWork.Repository<Inmate>().FindByIdAsync(inmateRequest.Id);
            if (inmate == null) return false;
            inmate.IsVisit = inmateRequest.IsVisit;
            inmate.AmountDue = inmateRequest.AmountDue;
            inmate.Savings = inmateRequest.Savings;
            return await _unitOfWork.Complete() > 0;
        }        
    }
}