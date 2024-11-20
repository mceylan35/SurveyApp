using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Options
{
    public class OptionDetailDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int VoteCount { get; set; }
        public double Percentage { get; set; }
    }
}
