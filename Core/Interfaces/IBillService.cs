using Core.CoreDtos;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBillService
    {
        Task GenerateIndividualBillsAsync(int month, int year);
        Task<MonthlyBillDto> GenerateMonthlyBillAsync(int month, int year);
        
    }
}