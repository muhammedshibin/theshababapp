using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    
    public class FeedbackController : BaseApiController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        [HttpPost]
        public async Task<ActionResult> AddFeedback(FeedBack feedBack)
        {
            feedBack.WrittenBy = User.FindFirstValue(ClaimTypes.Name);

            await _feedbackService.AddFeedBack(feedBack);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<FeedBack>> GetFeedbacks()
        {
            return Ok(await _feedbackService.GetFeedBacksAsync());
        }
    }
}
