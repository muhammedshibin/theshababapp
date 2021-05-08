using Core.CoreDtos;
using Core.DataFilters;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBillService
    {
        Task<bool> AcceptBillPayment(PaymentDto paymentDto);
        Task<IReadOnlyList<InmateBill>> GetBillsFOrInmateAsync(int inmateId);
        Task GenerateIndividualBillsAsync(int month, int year);
        Task<MonthlyBillDto> GenerateMonthlyBillAsync(int month, int year);
        Task<IReadOnlyList<InmateBill>> GetInmateBillsAsync(BillFilter filter);
        Task<int> GetInmateBillsCountAsync(BillFilter filter);


    }
}