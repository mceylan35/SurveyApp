using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Surveys
{
    public class SurveyDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OptionDetailDto> Options { get; set; }
        public int TotalVotes { get; set; }
    }
}
