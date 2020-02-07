﻿using alexander_neumann.api.Data;
using alexander_neumann.api.Data.Entities;
using alexander_neumann.api.Models;
using alexander_neumann.api.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace alexander_neumann.api.Features.TrainingRuns.Queries
{
    public class GetTrainingRunsStatsQuery : IRequest<TrainingRunStatisticsModel>
    {
    }

    public class GetTrainingRunsStatsQueryHandler : IRequestHandler<GetTrainingRunsStatsQuery, TrainingRunStatisticsModel>
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public GetTrainingRunsStatsQueryHandler(IConfiguration configuration, IMapper mapper, AppDbContext context, IUserService userService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
            _userService = userService;
        }

        public async Task<TrainingRunStatisticsModel> Handle(GetTrainingRunsStatsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrentUser();
            if (user == null)
                throw new ArgumentNullException("Current User is NULL.");

            var items = await _context.TrainingRuns.Where(x => x.UserId == user.Id).OrderBy(x => x.TrainingDate).ToListAsync();
            var models = _mapper.Map<List<TrainingRunModel>>(items);

            var stats = new TrainingRunStatisticsModel(user.Id, models);

            return stats;
        }
    }
}
