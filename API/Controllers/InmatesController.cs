using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InmatesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InmatesController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TransactionDto>>> GetTransactions([FromQuery]TransactionSpecParams specParams)
        {
            var spec = new TransactionWithCategoryAndVendorSpecification(specParams);
            var transactions = await  _unitOfWork.Repository<Transaction>().FindAllBySpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<TransactionDto>>(transactions));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var spec = new TransactionWithCategoryAndVendorSpecification(id);
            var transaction = await _unitOfWork.Repository<Transaction>().FindOneBySpecAsync(spec);
            return _mapper.Map<TransactionDto>(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> PostTransaction(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            _unitOfWork.Repository<Transaction>().Add(transaction);

            var vendor = await _unitOfWork.Repository<Vendor>().FindByIdAsync(transactionDto.PaidPartyId);

            if (vendor != null)
            {
                vendor.AmountInHand = transactionDto.IsExpense
                    ? vendor.AmountInHand - transactionDto.Amount
                    : vendor.AmountInHand + transactionDto.Amount;
                vendor.ModfiedOn = DateTime.Now;
            }

            await _unitOfWork.Complete();
            return transactionDto;
        }

    }
}