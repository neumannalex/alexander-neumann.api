using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alexander_neumann.api.Models
{
    public class TrainingRunModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public DateTime TrainingDate { get; set; }
        public TimeSpan Duration { get; set; }
        public double DistanceInMeters { get; set; }
        public double EnergyInKCal { get; set; }

        public int TrainingCalendarWeek { get; set; }
        public TimeSpan AveragePacePerKilometer { get; set; }
        public double AverageSpeedInKilometersPerHour { get; set; }

    }
}
