using alexander_neumann.api.Data.Entities;
using alexander_neumann.api.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alexander_neumann.api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TrainingRun, TrainingRunModel>();
        }
    }
}
