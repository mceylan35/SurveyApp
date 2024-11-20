using MediatR;
using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Application.Common.Interfaces;
using SurveyApp.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.VoteSurvey
{
    public record VoteSurveyCommand : IRequest<Result<bool>>
    {
        public Guid SurveyId { get; init; }
        public Guid OptionId { get; init; }
    }
}
