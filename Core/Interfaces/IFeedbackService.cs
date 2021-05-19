using Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFeedbackService
    {
        Task AddFeedBack(FeedBack feedback);
        Task<IReadOnlyList<FeedBack>> GetFeedBacksAsync();
    }
}
