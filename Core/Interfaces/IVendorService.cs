using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IVendorService
    {
        Task<bool> AddVendor(Vendor vendor);
        Task<bool> UpdateVendor(Vendor vendor);
        Task<IReadOnlyList<Vendor>> GetVendors();
        Task<Vendor> GetVendor(int id);
    }
}