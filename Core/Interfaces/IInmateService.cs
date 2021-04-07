using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataFilters;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IInmateService
    {
        public Task<IReadOnlyList<Inmate>> GetInmates(InmateFilter inmateFilter);
        public Task<Inmate> GetInMate(int id);
        public Task<bool> AddInmate(Inmate inmate);
    }
}