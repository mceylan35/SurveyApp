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
    public class VoteSurveyCommandHandler : IRequestHandler<VoteSurveyCommand, Result<bool>>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ICurrentUserService _currentUserService;

        public VoteSurveyCommandHandler(
            ISurveyRepository surveyRepository,
            ICurrentUserService currentUserService)
        {
            _surveyRepository = surveyRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result<bool>> Handle(VoteSurveyCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (!userId.HasValue)
                return Result<bool>.Failure("User not authenticated");

            var hasVoted = await _surveyRepository.HasUserVotedAsync(request.SurveyId, userId.Value);
            if (hasVoted)
                return Result<bool>.Failure("User has already voted in this survey");

            var survey = await _surveyRepository.GetByIdAsync(request.SurveyId);
            if (survey == null)
                return Result<bool>.Failure("Survey not found");

            survey.Vote(userId.Value, request.OptionId);
            await _surveyRepository.UpdateAsync(survey);

            return Result<bool>.Success(true);
        }
    }
}
