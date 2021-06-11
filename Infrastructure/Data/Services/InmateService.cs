using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public InmateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            //inmate.IsVisit = inmateRequest.IsVisit;
            //inmate.AmountDue = inmateRequest.AmountDue;
            //inmate.Savings = inmateRequest.Savings;
            return await _unitOfWork.Complete() > 0;
        }

        public async Task<bool> UpdateInmatePhoto(string PhotoUrl, int inmateId)
        {
            var inmate = await _unitOfWork.Repository<Inmate>().FindByIdAsync(inmateId);
            if (inmate == null) return false;
            inmate.PictureUrl = PhotoUrl;
            return await _unitOfWork.Complete() > 0;
        }
    }
}