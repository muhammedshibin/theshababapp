using Core.CoreDtos;
using Core.Interfaces;
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

        public BillsController(IBillService billservice)
        {
            _billservice = billservice;
        }
        // GET: api/<BillsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BillsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
