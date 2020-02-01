using alexander_neumann.api.Data;
using alexander_neumann.api.Data.Entities;
using alexander_neumann.api.Models;
using alexander_neumann.api.Services;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace alexander_neumann.api.Features.TrainingRuns.Commands
{
    public class TrainingRunParameters
    {
        public DateTime TrainingDate { get; set; }
        public double DurationInSeconds { get; set; }
        public double DistanceInMeters { get; set; }
        public double EnergyInKCal { get; set; }
    }

    public class CreateTrainingRunsCommand :  IRequest<List<TrainingRun>>
    {
        public List<TrainingRunParameters> TrainingRuns { get; set; }
    }

    public class CreateTrainingRunsCommandHandler : IRequestHandler<CreateTrainingRunsCommand, List<TrainingRun>>
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public CreateTrainingRunsCommandHandler(IConfiguration configuration, IMapper mapper, AppDbContext context, IUserService userService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
            _userService = userService;
        }

        public async Task<List<TrainingRun>> Handle(CreateTrainingRunsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUser();
            if (user == null)
                throw new ArgumentNullException("Current User is NULL.");

            var items = new List<TrainingRun>();

            foreach(var run in request.TrainingRuns)
            {
                var id = _context.GetNewId();
                var item = new TrainingRun
                {
                    Id = id,
                    UserId = user.Id,
                    TrainingDate = run.TrainingDate,
                    Duration = TimeSpan.FromSeconds(run.DurationInSeconds),
                    DistanceInMeters = run.DistanceInMeters,
                    EnergyInKCal = run.EnergyInKCal
                };

                await _context.TrainingRuns.AddAsync(item, cancellationToken);
                items.Add(item);
            }
            
            await _context.SaveChangesAsync(cancellationToken);

            return items;
        }
    }
}
