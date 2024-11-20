using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.VoteSurvey
{
    public class VoteSurveyCommandValidator : AbstractValidator<VoteSurveyCommand>
    {
        public VoteSurveyCommandValidator()
        {
            RuleFor(x => x.SurveyId).NotEmpty();
            RuleFor(x => x.OptionId).NotEmpty();
        }
    }
}
