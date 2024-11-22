using MediatR;
using SurveyApp.Application.Common.Results;
using SurveyApp.Application.DTOs.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetSurveyDetail
{
    public record GetSurveyDetailQuery(Guid Id) : IRequest<Result<SurveyDetailDto>>;

}
