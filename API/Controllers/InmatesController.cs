using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using Core.DataFilters;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Core.CoreDtos;

namespace API.Controllers
{
   
    public class InmatesController : BaseApiController
    {
        
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IInmateService _inmateService;

        public InmatesController(IInmateService inmateService,IMapper mapper,IWebHostEnvironment env)
        {
            _mapper = mapper;
            _env = env;
            _inmateService = inmateService;
        }
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InmateDto>>> GetInmates([FromQuery]InmateFilter inmateFilter)
        {
            var count = await _inmateService.GetInmatesCount(inmateFilter);
            var inmates = await _inmateService.GetInmates(inmateFilter);

            Response.AddPaginationHeader(count, inmateFilter.PageIndex, inmateFilter.PageSize);

            return Ok(inmates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InmateDto>> GetInmate(int id)
        {
            var inmates = await _inmateService.GetInmate(id);
            return Ok(_mapper.Map<InmateDto>(inmates));
        }

        [HttpPost]
        public async Task<ActionResult<InmateDto>> AddInmate(InmateDto inmateDto)
        {
            var imageUrl = string.Empty;

            if(inmateDto.InmatePhoto != null)
            {
                IFormFile file = inmateDto.InmatePhoto;
                var UrlHost = _env.WebRootPath;
                var location = Path.Combine(UrlHost, "Images");
                var ImageUrl = Path.Combine(location, file.FileName);
                await inmateDto.InmatePhoto.CopyToAsync(new FileStream(ImageUrl, FileMode.Create));
                imageUrl = @"Images/" + file.FileName;
            }

            var inmate = _mapper.Map<Inmate>(inmateDto);
            inmate.PictureUrl = imageUrl;

            var created = await _inmateService.AddInmate(inmate);

            if (created) return Created("Success", inmateDto);

            return BadRequest();
        }

        [HttpPatch]
        public async Task<ActionResult<InmateDto>> UpdateInmate(InmateDto inmateDto)
        {
            var imageUrl = string.Empty;

            if (inmateDto.InmatePhoto != null)
            {
                IFormFile file = inmateDto.InmatePhoto;
                var UrlHost = _env.WebRootPath;
                var location = Path.Combine(UrlHost, "Images");
                var ImageUrl = Path.Combine(location, file.FileName);
                await inmateDto.InmatePhoto.CopyToAsync(new FileStream(ImageUrl, FileMode.Create));
                imageUrl = @"Images/" + file.FileName;
            }

            var inmate = _mapper.Map<Inmate>(inmateDto);
            inmate.PictureUrl = imageUrl;

            var created = await _inmateService.UpdateInmate(inmate);

            if (created) return Ok(inmateDto);

            return BadRequest("Error why");
        }        

    }
}