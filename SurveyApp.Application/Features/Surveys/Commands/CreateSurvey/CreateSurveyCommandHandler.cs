using MediatR;
using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Application.Common.Interfaces.Services;
using SurveyApp.Application.Common.Results;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.CreateSurvey
{
    public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, Result<bool>>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOptionRepository _optionRepository;

        public CreateSurveyCommandHandler(
            ISurveyRepository surveyRepository,
            IOptionRepository optionRepository,
            ICurrentUserService currentUserService)
        {
            _surveyRepository = surveyRepository;
            _optionRepository = optionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result<bool>> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (!userId.HasValue)
                return Result<bool>.Failure("User not authenticated");

            var survey = Survey.Create(request.Title, userId.Value);
            await _surveyRepository.CreateAsync(survey);
            await _optionRepository.CreateOptionsForSurvey(survey.Id, request.Options);
            return Result<bool>.Success(true);
        }
    }
}
