using API.Dtos;
using API.Extensions;
using AutoMapper;
using Core.CoreDtos;
using Core.DataFilters;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class TransactionsController : BaseApiController
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TransactionDto>>> GetTransactions([FromQuery] TransactionsFilter filter)
        {
            var transactions = await _transactionService.GetTransactions(filter);
            int transactionsCount = await _transactionService.GetTransactionsCount(filter);
            Response.AddPaginationHeader(transactionsCount, filter.PageIndex, filter.PageSize);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransaction(id);
            return _mapper.Map<TransactionDto>(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> PostTransaction(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<TransactionDetail>(transactionDto);
            await _transactionService.PostTransaction(transaction);
            return transactionDto;
        }

        [HttpPatch]
        public async Task<ActionResult<TransactionDto>> UpdateTransaction(TransactionDto transactionDto)
        {
            var updated = _mapper.Map<TransactionDetail>(transactionDto);

            return Ok(await _transactionService.UpdateTransaction(updated));

        }
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetTransactionCategories()
        {
            var transactionCategories = await _transactionService.GetTransactionCategories();
            return Ok(transactionCategories);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTransation(int id)
        {
            var deleted = await _transactionService.DeleteTransaction(id);

            if (!deleted) return BadRequest(new ErrorResponse(400, "Failed to delete the transaction"));

            return Ok(true);
        }
    }
}
