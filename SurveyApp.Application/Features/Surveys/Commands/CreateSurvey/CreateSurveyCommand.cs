using MediatR;
using SurveyApp.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.CreateSurvey
{
    public record CreateSurveyCommand : IRequest<Result<bool>>
    {
        public string Title { get; init; }
        public List<string> Options { get; init; }
    }
}
