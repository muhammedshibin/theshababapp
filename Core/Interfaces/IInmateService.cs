using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CoreDtos;
using Core.DataFilters;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IInmateService
    {
        public Task<IReadOnlyList<InmateToReturnDto>> GetInmates(InmateFilter inmateFilter);
        public Task<int> GetInmatesCount(InmateFilter filter);
        public Task<Inmate> GetInmate(int id);
        public Task<bool> AddInmate(Inmate inmate);
        public Task<bool> UpdateInmate(Inmate inmate);
    }
}