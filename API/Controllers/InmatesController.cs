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
        private readonly IPhotoService _photoService;
        private readonly IInmateService _inmateService;

        public InmatesController(IInmateService inmateService,IMapper mapper,IWebHostEnvironment env, IPhotoService photoService)
        {
            _mapper = mapper;
            _env = env;
            _photoService = photoService;
            _inmateService = inmateService;
        }
        
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
            var inmate = await _inmateService.GetInmate(id);
            if(inmate == null) return NotFound(new ErrorResponse(401,"Inmate Not Found"));
            return Ok(_mapper.Map<InmateDto>(inmate));
        }

        [HttpPost]
        public async Task<ActionResult<InmateDto>> AddInmate(InmateDto inmateDto)
        {                  

            var inmate = _mapper.Map<Inmate>(inmateDto);

            var createdInmate = await _inmateService.AddInmate(inmate);           

            return Ok(_mapper.Map<InmateDto>(createdInmate));
            
        }

        [HttpPatch("photo/{inmateId}")]
        public async Task<ActionResult<bool>> UpdateInmatePhoto(int inmateId)
        {
            var imageUrl = string.Empty;
            var file = Request.Form.Files[0];

            var imageUploadResult = await  _photoService.AddPhotoAsync(file);

            if(imageUploadResult.Error != null)
            {
                return BadRequest(new ErrorResponse(400, imageUploadResult.Error.Message));
            }

            imageUrl = imageUploadResult.SecureUrl.AbsoluteUri;
            
            var updated = await _inmateService.UpdateInmatePhoto(imageUrl, inmateId);

            if (updated) return Ok(true);

            return BadRequest("Error Occured");
        }



        [HttpPatch]
        public async Task<ActionResult<InmateDto>> UpdateInmate(InmateDto inmateDto)
        {
           

            var inmate = _mapper.Map<Inmate>(inmateDto);
           
            var created = await _inmateService.UpdateInmate(inmate);

            if (created) return Ok(inmateDto);

            return BadRequest();
        }
        [AllowAnonymous]
        [HttpPost("deactivate")]
        public async Task<ActionResult<bool>> DeactivateInmate(DeactivateInmateDto deactivateInmateDto)
        {
            var deactivatedInmate = _mapper.Map<DeactivateInmateDto, DeactivatedInmate>(deactivateInmateDto);


            var deactivated = await _inmateService.DeactivateInmate(deactivatedInmate);

            return Ok(deactivated);

        }

    }
}