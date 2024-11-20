using MediatR;
using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetSurveyList
{
    public class GetSurveyListQueryHandler : IRequestHandler<GetSurveyListQuery, Result<List<SurveyDto>>>
    {
        private readonly ISurveyRepository _surveyRepository;

        public GetSurveyListQueryHandler(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<Result<List<SurveyDto>>> Handle(GetSurveyListQuery request, CancellationToken cancellationToken)
        {
            var surveys = await _surveyRepository.GetAllAsync();

            var surveyDtos = surveys.Select(s => new SurveyDto
            {
                Id = s.Id,
                Title = s.Title,
                Options = s.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Text = o.Text,
                    VoteCount = o.VoteCount
                }).ToList()
            }).ToList();

            return Result<List<SurveyDto>>.Success(surveyDtos);
        }
    }
}
