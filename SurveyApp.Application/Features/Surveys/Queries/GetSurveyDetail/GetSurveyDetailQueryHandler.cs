using MediatR;
using SurveyApp.Application.Common.Interfaces;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetSurveyDetail
{
    public class GetSurveyDetailQueryHandler : IRequestHandler<GetSurveyDetailQuery, SurveyDetailDto>
    {
        private readonly IMongoDbContext _context;

        public GetSurveyDetailQueryHandler(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task<SurveyDetailDto> Handle(GetSurveyDetailQuery request, CancellationToken cancellationToken)
        {
            var survey = await _context.Surveys
                .Find(s => s.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (survey == null)
                throw new NotFoundException(nameof(Survey), request.Id);

            var totalVotes = survey.Options.Sum(o => o.VoteCount);

            return new SurveyDetailDto
            {
                Id = survey.Id,
                Title = survey.Title,
                CreatorId = survey.CreatorId,
                CreatedAt = survey.CreatedAt,
                Options = survey.Options.Select(o => new OptionDetailDto
                {
                    Id = o.Id,
                    Text = o.Text,
                    VoteCount = o.VoteCount,
                    Percentage = totalVotes > 0 ? (o.VoteCount * 100.0 / totalVotes) : 0
                }).ToList(),
                TotalVotes = totalVotes
            };
        }
    }
}
