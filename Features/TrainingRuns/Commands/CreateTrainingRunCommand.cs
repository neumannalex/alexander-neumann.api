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
    public class CreateTrainingRunCommand : IRequest<TrainingRun>
    {
        public DateTime TrainingDate { get; set; }
        public double DurationInSeconds { get; set; }
        public double DistanceInMeters { get; set; }
        public double EnergyInKCal { get; set; }
    }

    public class CreateTrainingRunCommandValidator : AbstractValidator<CreateTrainingRunCommand>
    {
        public CreateTrainingRunCommandValidator()
        {
            // e. g.
            RuleFor(c => c.TrainingDate)
               .NotEmpty().WithMessage("TrainingDate is required.");

            RuleFor(c => c.DurationInSeconds)
                .NotEmpty().WithMessage("DurationInSeconds is required.")
                .GreaterThan(0).WithMessage("DurationInSeconds must be greater than zero.");

            RuleFor(c => c.DistanceInMeters)
                .NotEmpty().WithMessage("DistanceInMeters is required.")
                .GreaterThan(0).WithMessage("DistanceInMeters must be greater than zero.");

            RuleFor(c => c.EnergyInKCal)
                 .GreaterThan(0).WithMessage("EnergyInKCal must be greater than zero.");
        }
    }

    public class CreateTrainingRunCommandHandler : IRequestHandler<CreateTrainingRunCommand, TrainingRun>
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public CreateTrainingRunCommandHandler(IConfiguration configuration, IMapper mapper, AppDbContext context, IUserService userService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
            _userService = userService;
        }

        public async Task<TrainingRun> Handle(CreateTrainingRunCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUser();
            if (user == null)
                throw new ArgumentNullException("Current User is NULL.");


            var id = _context.GetNewId();

            var item = new TrainingRun
            {
                Id = id,
                UserId = user.Id,
                TrainingDate = request.TrainingDate,
                Duration = TimeSpan.FromSeconds(request.DurationInSeconds),
                DistanceInMeters = request.DistanceInMeters,
                EnergyInKCal = request.EnergyInKCal
            };

            await _context.TrainingRuns.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
