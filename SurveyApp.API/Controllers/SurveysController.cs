using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Features.Surveys.Commands.CreateSurvey;
using SurveyApp.Application.Features.Surveys.Commands.VoteSurvey;
using SurveyApp.Application.Features.Surveys.Queries.GetSurveyDetail;
using SurveyApp.Application.Features.Surveys.Queries.GetSurveyList;

namespace SurveyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SurveysController : BaseController
    {
        IMediator _mediator;
        public SurveysController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetSurveyListQuery query)
        {
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSurveyCommand command)
        {
            return HandleCreatedResult(
                await _mediator.Send(command));
        }

        [HttpPost("{id}/vote")]
        public async Task<IActionResult> Vote(Guid id, VoteSurveyCommand command)
        {
            if (id != command.SurveyId)
                return BadRequest("Invalid survey ID");

            return HandleResult(await _mediator.Send(command));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            var query = new GetSurveyDetailQuery(id); 
            return HandleResult(await _mediator.Send(query));
        }
    }
}

