using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Votes
{
    public class VoteDto
    {
        public Guid UserId { get; set; }
        public Guid OptionId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
