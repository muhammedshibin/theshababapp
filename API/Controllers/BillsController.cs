using API.Dtos;
using API.Extensions;
using AutoMapper;
using Core.CoreDtos;
using Core.DataFilters;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    
    public class BillsController : BaseApiController
    {
        private readonly IBillService _billservice;
        private readonly IMapper _mapper;

        public BillsController(IBillService billservice , IMapper mapper)
        {
            _billservice = billservice;
            _mapper = mapper;
        }
        // GET: api/<BillsController>
        [HttpGet("inmate/{inmateId}")]
        public async Task<ActionResult<BillDto>> GetBillsForInmate(int inmateId)
        {
            var bills = await _billservice.GetBillsFOrInmateAsync(inmateId);
            var billsToReturnDto = _mapper.Map<IReadOnlyList<BillDto>>(bills);
            return Ok(billsToReturnDto);
        }

        // GET api/<BillsController>/5
        [HttpGet("total/{month}/{year}")]
        public async Task<ActionResult<MonthlyBillDto>> GenerateMonthlyBill(int month,int year)
        {
            return  await _billservice.GenerateMonthlyBillAsync(month, year);
        }

        // POST api/<BillsController>
        [HttpPost("{month}/{year}")]
        public async Task<ActionResult> Post(int month , int year)
        {
            await _billservice.GenerateIndividualBillsAsync(month, year);
            return Ok();
        }

        // PUT api/<BillsController>/5
        [HttpPost("payment")]
        public async Task<ActionResult<bool>> AcceptPayments(PaymentDto paymentDto)
        {
            var paid = await  _billservice.AcceptBillPayment(paymentDto);

            if (!paid) throw new Exception("Error occured during payment");

            return Ok(true);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BillDto>>> GetBills([FromQuery]BillFilter filter)
        {
            var bills = await _billservice.GetInmateBillsAsync(filter);
            var count = await _billservice.GetInmateBillsCountAsync(filter);

            Response.AddPaginationHeader(count, filter.PageIndex, filter.PageSize);

            var billsToReturnDto = _mapper.Map<IReadOnlyList<BillDto>>(bills);

            return Ok(billsToReturnDto);
        }

        // DELETE api/<BillsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
