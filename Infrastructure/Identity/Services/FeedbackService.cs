using Core.Identity;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly AppUserIdentityDbContext _context;

        public FeedbackService(AppUserIdentityDbContext context)
        {
            _context = context;
        }
        public async Task AddFeedBack(FeedBack feedback)
        {
            _context.FeedBacks.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<FeedBack>> GetFeedBacksAsync()
        {
            return await _context.FeedBacks.ToListAsync();
        }
    }
}
