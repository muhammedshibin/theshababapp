using API.Dtos;
using AutoMapper;
using Core.DataFilters;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<IReadOnlyList<TransactionDto>>> GetTransactions([FromQuery] TransactionsFilter specParams)
        {
            var transactions = await _transactionService.GetTransactions(specParams);
            return Ok(_mapper.Map<IReadOnlyList<TransactionDto>>(transactions));
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
    }
}
