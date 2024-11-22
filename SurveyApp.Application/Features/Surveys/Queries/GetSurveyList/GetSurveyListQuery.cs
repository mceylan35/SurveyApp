using MediatR;
using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Application.Common.Models;
using SurveyApp.Application.Common.Results;
using SurveyApp.Application.DTOs.Options;
using SurveyApp.Application.DTOs.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetSurveyList
{
    public class GetSurveyListQuery : IRequest<Result<PaginatedList<SurveyDto>>>
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 5;
    }
}
