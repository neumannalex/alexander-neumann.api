using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alexander_neumann.api.Features.TrainingRuns.Commands;
using alexander_neumann.api.Features.TrainingRuns.Queries;
using alexander_neumann.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alexander_neumann.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingRunsController : ApiController
    {
        [HttpGet]
        public async Task<List<TrainingRunModel>> GetAll()
        {
            var query = new GetTrainingRunsQuery();

            var items = await Mediator.Send(query);
            var model = Mapper.Map<List<TrainingRunModel>>(items);

            return model;
        }

        [HttpPost]
        public async Task<TrainingRunModel> Create(CreateTrainingRunCommand command)
        {
            var item = await Mediator.Send(command);
            var model = Mapper.Map<TrainingRunModel>(item);

            return model;
        }

        [HttpPost("bulk")]
        public async Task<List<TrainingRunModel>> CreateMany(CreateTrainingRunsCommand command)
        {
            var item = await Mediator.Send(command);
            var model = Mapper.Map<List<TrainingRunModel>>(item);

            return model;
        }
    }
}