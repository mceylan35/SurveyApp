using MediatR;
using SurveyApp.Application.Common.Interfaces.Repositories;
using SurveyApp.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyApp.Application.Common.Interfaces.Services;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Features.Surveys.Commands.VoteSurvey
{
    public class VoteSurveyCommandHandler : IRequestHandler<VoteSurveyCommand, Result<bool>>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly ICurrentUserService _currentUserService;

        public VoteSurveyCommandHandler(
            ISurveyRepository surveyRepository,
            IVoteRepository voteRepository,
            IOptionRepository optionRepository,
            ICurrentUserService currentUserService)
        {
            _surveyRepository = surveyRepository;
            _voteRepository = voteRepository;
            _optionRepository = optionRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result<bool>> Handle(VoteSurveyCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (!userId.HasValue)
                return Result<bool>.Failure("User not authenticated");

            var hasVoted = await _voteRepository.HasUserVotedAsync(request.SurveyId, userId.Value);
            if (hasVoted)
                return Result<bool>.Failure("User has already voted in this survey");

            var survey = await _surveyRepository.GetByIdAsync(request.SurveyId);
            if (survey == null)
                return Result<bool>.Failure("Survey not found");

            var option = await _optionRepository.GetByIdAsync(request.OptionId);
            if (option == null || option.SurveyId != request.SurveyId)
                return Result<bool>.Failure("Option not found or does not belong to this survey");

            var vote = Vote.Create(userId.Value, request.OptionId, request.SurveyId);

            await _voteRepository.AddVoteAsync(vote);
            await _optionRepository.IncrementVoteCount(request.OptionId);

            return Result<bool>.Success(true);
        }
    }
}
