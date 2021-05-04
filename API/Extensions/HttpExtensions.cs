using API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int count, int pageNumber, int pageSize)
        {
            var paginationDto = new PaginationDto
            {
                Count = count,
                PageNumber = pageNumber,
                PageSize = pageSize                
            };
            
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var paginatedString = JsonSerializer.Serialize(paginationDto,serializerOptions);

            response.Headers.Add("Pagination", paginatedString);
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
