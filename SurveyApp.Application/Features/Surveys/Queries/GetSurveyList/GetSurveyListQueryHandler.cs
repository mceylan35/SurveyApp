using MediatR;
using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Application.Common.Models;
using SurveyApp.Application.Common.Results; 
using SurveyApp.Application.DTOs.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetSurveyList
{
    public class GetSurveyListQueryHandler : IRequestHandler<GetSurveyListQuery, Result<PaginatedList<SurveyDto>>>
    {
        private readonly ISurveyRepository _surveyRepository;

        public GetSurveyListQueryHandler(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<Result<PaginatedList<SurveyDto>>> Handle(
            GetSurveyListQuery request,
            CancellationToken cancellationToken)
        {
            var (surveys, totalCount) = await _surveyRepository.GetPaginatedListAsync(
                request.Page,
                request.PageSize);

            var surveyDtos = surveys.Select(s => new SurveyDto
            {
                Id = s.Id,
                Title = s.Title
             }).ToList();

            var paginatedList = new PaginatedList<SurveyDto>(
                surveyDtos,
                totalCount,
                request.Page,
                request.PageSize);

            return Result<PaginatedList<SurveyDto>>.Success(paginatedList);
        }
    }
}
