using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using SurveyApp.Application.Common.Interfaces;
using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Application.Common.Models;
using SurveyApp.Application.Common.Results;
using SurveyApp.Application.DTOs.Options;
using SurveyApp.Application.DTOs.Surveys;
using SurveyApp.Domain.Entities;
using SurveyApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetSurveyDetail
{
    public class GetSurveyDetailQueryHandler : IRequestHandler<GetSurveyDetailQuery, Result<SurveyDetailDto>>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IOptionRepository _optionRepository;

        public GetSurveyDetailQueryHandler(
            ISurveyRepository surveyRepository,
            IOptionRepository optionRepository)
        {
            _surveyRepository = surveyRepository;
            _optionRepository = optionRepository;
        }

        public async Task<Result<SurveyDetailDto>> Handle(GetSurveyDetailQuery request, CancellationToken cancellationToken)
        {
          var surveyEntity = await  _surveyRepository.GetByIdAsync(request.Id);
         
            if (surveyEntity == null)
                throw new NotFoundException(nameof(Survey));



            var options = await _optionRepository.GetBySurveyIdAsync(surveyEntity.Id);
            var totalVotes = options.Sum(o => o.VoteCount);

            var surveyDetail = new SurveyDetailDto
            {
                Id = surveyEntity.Id,
                Title = surveyEntity.Title,
                CreatorId = surveyEntity.CreatorId,
                CreatedAt = surveyEntity.CreatedAt,
                Options = options.Select(o => new OptionDetailDto
                {
                    Id = o.Id,
                    Text = o.Text,
                    VoteCount = o.VoteCount,
                    Percentage = totalVotes > 0 ? (o.VoteCount * 100.0 / totalVotes) : 0
                }).ToList(),
                TotalVotes = totalVotes
            };

            return Result<SurveyDetailDto>.Success(surveyDetail);
        }
    }
}
