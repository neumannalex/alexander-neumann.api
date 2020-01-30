using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alexander_neumann.api.Models
{
    public class TrainingRunStatisticsModel
    {
        public string UserId { get; set; }

        public List<TrainingRunModel> Items { get; set; } = new List<TrainingRunModel>();
        public int Total
        {
            get { return Items == null ? 0 : Items.Count; }
        }

        public double AverageSpeedInKilometersPerHour
        {
            get
            {
                return 0;
            }
        }

        public double MinimumSpeedInKilometersPerHour
        {
            get
            {
                return 0;
            }
        }

        public double MaximumSpeedInKilometersPerHour
        {
            get
            {
                return 0;
            }
        }


        public double AveragePaceInMinutesPerKilometer
        {
            get
            {
                return 0;
            }
        }

        public double MaximumPaceInMinutesPerKilometer
        {
            get
            {
                return 0;
            }
        }

        public double MinimumPaceInMinutesPerKilometer
        {
            get
            {
                return 0;
            }
        }

        public double AverageDistanceInMeters
        {
            get
            {
                return 0;
            }
        }

        public double MinimumDistanceInMeters
        {
            get
            {
                return 0;
            }
        }

        public double MaximumDistanceInMeters
        {
            get
            {
                return 0;
            }
        }

        public double TotalDistanceInMeters
        {
            get
            {
                return 0;
            }
        }

        public TimeSpan AverageDuration
        {
            get
            {
                return TimeSpan.Zero;
            }
        }

        public TimeSpan MinimumDuration
        {
            get
            {
                return TimeSpan.Zero;
            }
        }

        public TimeSpan MaximumDuration
        {
            get
            {
                return TimeSpan.Zero;
            }
        }

        public TimeSpan TotalDuration
        {
            get
            {
                return TimeSpan.Zero;
            }
        }

        public double AverageEnergyInKCal
        {
            get
            {
                return 0;
            }
        }

        public double MinimumEnergyInKCal
        {
            get
            {
                return 0;
            }
        }

        public double MaximumEnergyInKCal
        {
            get
            {
                return 0;
            }
        }

        public double TotalEnergyInKCal
        {
            get
            {
                return 0;
            }
        }
    }
}
