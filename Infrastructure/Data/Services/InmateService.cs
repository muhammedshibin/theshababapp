using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IReadOnlyList<Inmate>> GetInmates(InmateFilter filter)
        {
            var spec = new InmateSpecification(filter);
            return await  _unitOfWork.Repository<Inmate>().FindAllBySpecAsync(spec);
        }

        public async Task<Inmate> GetInMate(int id)
        {
            return await _unitOfWork.Repository<Inmate>().FindByIdAsync(id);
        }

        public async Task<bool> AddInmate(Inmate inmate)
        {
            _unitOfWork.Repository<Inmate>().Add(inmate);
            return await _unitOfWork.Complete() > 0;
        }
    }
}