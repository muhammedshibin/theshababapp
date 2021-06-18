using Core.Enumerations;
using Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.RequiresAdminRole)]
    public class AdminController : BaseApiController
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet("user-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                        .Include(x => x.UserRoles)
                        .ThenInclude(x => x.Role)
                        .Select(x => new
                        {
                            DisplayName = x.DisplayName,
                            UserName = x.UserName,
                            Roles = x.UserRoles.Select(x => x.Role.Name).ToList()
                        })
                        .ToListAsync();

            return Ok(users);
        }

        [HttpPost("edit-user-roles/{userName}")]
        public async Task<ActionResult> EditUserRole(string userName, [FromQuery] string roles)
        {
            var appUser = await _userManager.FindByNameAsync(userName);

            if(appUser == null) return NotFound(new ErrorResponse(404, "User Not Found with provided Username"));
            
            var userRoles = await _userManager.GetRolesAsync(appUser);

            var rolesRequired = roles.Split(",").ToArray();

            var rolesAddedResult = await _userManager.AddToRolesAsync(appUser, rolesRequired.Except(userRoles));

            if (!rolesAddedResult.Succeeded)
            {
                var errorResponse = new ErrorResponse
                {
                    IsError = true,
                    StatusCode = 400,
                    UserMessage = "Error Occured , Please see the details below",
                    Errors = rolesAddedResult.Errors.Select(e => e.Description).ToList()
                };

                return BadRequest(errorResponse);
            }

            var rolesRemovedResult = await _userManager.RemoveFromRolesAsync(appUser, userRoles.Except(rolesRequired));
            if (!rolesRemovedResult.Succeeded)
            {
                var errorResponse = new ErrorResponse
                {
                    IsError = true,
                    StatusCode = 400,
                    UserMessage = "Error Occured , Please see the details below",
                    Errors = rolesRemovedResult.Errors.Select(e => e.Description).ToList()
                };

                return BadRequest(errorResponse);
            }

            return Ok(await _userManager.GetRolesAsync(appUser));
        }
    }
}
