using API.Dtos;
using Core.Enumerations;
using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService, 
            IEmailService emailService,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _config = config;
        }
        [Authorize(Policy = AuthorizationPolicies.RequiresAdminRole)]
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(RegisterDto registerDto)
        {
            var doesExist = await _userManager.Users
                .AnyAsync(x =>
                x.NormalizedUserName == _userManager.NormalizeName(registerDto.UserName)
                || x.NormalizedEmail == _userManager.NormalizeEmail(registerDto.Email));

            if (doesExist) return BadRequest(new ErrorResponse(400, "Username/Email already Taken"));

            var user = new AppUser
            {
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errorResponse = new ErrorResponse
                {
                    IsError = true,
                    StatusCode = 400,
                    UserMessage = "Error Occured , Please see the details below",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };

                return BadRequest(errorResponse);
            }

            var rolesResult = await _userManager.AddToRoleAsync(user, UserRoles.Member);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.Action("Confirm-Email", "Account", new { UserId = user.Id, Token = token }, Request.Scheme,_config["ApiUrl"]);

            await _emailService.SendEmailAsync(registerDto.Email, "Email Confirmation", confirmationLink,MailTemplates.ConfirmEmail);

            return Ok();
        }



        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null) return Unauthorized(new ErrorResponse(401, "You are Not Authorized"));

            if (!user.EmailConfirmed && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized(new ErrorResponse(401, "Email Not Confirmed"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ErrorResponse(401, "You are Not Authorized"));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateToken(user),
                UserName = user.UserName
            };
        }
        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUserDetails()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);

            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return Unauthorized(new ErrorResponse(401, "You are Not Authorized"));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateToken(user),
                UserName = user.UserName
            };
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<ActionResult<string>> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || token == null)
            {
                return NotFound(new ErrorResponse(404, "Invalid Request"));
            }

            var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, token);

            if (!confirmEmailResult.Succeeded) return Unauthorized(new ErrorResponse(401, "Invalid Token"));

            return Ok("Thank you for confirming your Email");
        }

        [AllowAnonymous]
        [HttpGet("forgot-password")]
        public async Task<ActionResult<string>> GenerateForgotPasswordToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return NotFound(new ErrorResponse(401, "User not Found"));

            if (!user.EmailConfirmed) return Unauthorized(new ErrorResponse(401, "You have not confirmed your email Account"));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPasswordLink = Url.ActionLink(action: "reset-password", controller: "account", values: new { token = token }, host: Request.Host.Value);

            await _emailService.SendEmailAsync(email, "Reset Password", resetPasswordLink,MailTemplates.ForgotPassword);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            if (user == null) return NotFound(new ErrorResponse(401, "User not Found"));

            var result = await _userManager.ResetPasswordAsync(user, forgotPasswordDto.Token, forgotPasswordDto.Password);

            if (!result.Succeeded) return BadRequest
                 (
                         new ErrorResponse(400, "Invalid Request")
                         {
                             Errors = result.Errors.Select(x => x.Description).ToList()
                         }
                 );

            return Ok();
        }

    }
}
