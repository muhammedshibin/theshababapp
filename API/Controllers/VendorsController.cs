using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class VendorsController : BaseApiController
    {
        private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;

        public VendorsController(IVendorService vendorService, IMapper mapper)
        {
            _vendorService = vendorService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<VendorDto>> CreateVendorAccount(VendorDto vendorDto)
        {
            var vendor = _mapper.Map<Vendor>(vendorDto);

            var created = await _vendorService.AddVendor(vendor);

            if (created) return vendorDto;

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<VendorDto>>> GetVendorDetails()
        {
            var vendors = await _vendorService.GetVendors();
            return Ok(_mapper.Map<IReadOnlyList<VendorDto>>(vendors));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VendorDto>> GetVendorById(int id)
        {
            var vendors = await _vendorService.GetVendor(id);
            return Ok(_mapper.Map<VendorDto>(vendors));
        }


    }
}
