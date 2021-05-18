using API.Dtos;
using AutoMapper;
using Core.DataFilters;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class LeavesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeavesController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveDto>> GetLeave(int id)
        {
            var filter = new LeavesFilter { LeaveId = id };

            var spec = new LeaveWithInmateSpecification(filter);
         
            return  await  _unitOfWork.Repository<Leave>().FindOneBySpecAsync<LeaveDto>(spec);
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<IReadOnlyList<LeaveDto>>> GetLeaves(int tenantId)
        {
            var filter = new LeavesFilter { InmateId = tenantId };

            var spec = new LeaveWithInmateSpecification(filter);

            return Ok(await _unitOfWork.Repository<Leave>().FindAllBySpecAsync<LeaveDto>(spec));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddLeave(LeaveDto leaveDto)
        {
            var leave = _mapper.Map<LeaveDto, Leave>(leaveDto);

            _unitOfWork.Repository<Leave>().Add(leave);

            return await _unitOfWork.Complete() > 0;
        }


    }
}
