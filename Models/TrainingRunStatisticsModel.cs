using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alexander_neumann.api.Models
{
    public class TrainingRunStatisticsModel
    {
        public TrainingRunStatisticsModel(string userId, List<TrainingRunModel> items)
        {
            UserId = userId;
            Items = items;
        }

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
                return Items == null ? 0 : Items.Average(x => x.AverageSpeedInKilometersPerHour);
            }
        }

        public double MinimumSpeedInKilometersPerHour
        {
            get
            {
                return Items == null ? 0 : Items.Min(x => x.AverageSpeedInKilometersPerHour);
            }
        }

        public double MaximumSpeedInKilometersPerHour
        {
            get
            {
                return Items == null ? 0 : Items.Max(x => x.AverageSpeedInKilometersPerHour);
            }
        }


        public TimeSpan AveragePaceInMinutesPerKilometer
        {
            get
            {
                if (Items == null)
                    return TimeSpan.Zero;

                var durationPerKilometerInSec = Items.Average(x => x.AveragePacePerKilometer.TotalSeconds);
                return TimeSpan.FromSeconds(durationPerKilometerInSec);
            }
        }

        public TimeSpan MaximumPaceInMinutesPerKilometer
        {
            get
            {
                if (Items == null)
                    return TimeSpan.Zero;

                var durationPerKilometerInSec = Items.Min(x => x.AveragePacePerKilometer.TotalSeconds);
                return TimeSpan.FromSeconds(durationPerKilometerInSec);
            }
        }

        public TimeSpan MinimumPaceInMinutesPerKilometer
        {
            get
            {
                if (Items == null)
                    return TimeSpan.Zero;

                var durationPerKilometerInSec = Items.Max(x => x.AveragePacePerKilometer.TotalSeconds);
                return TimeSpan.FromSeconds(durationPerKilometerInSec);
            }
        }

        public double AverageDistanceInMeters
        {
            get
            {
                return Items == null ? 0 : Items.Average(x => x.DistanceInMeters);
            }
        }

        public double MinimumDistanceInMeters
        {
            get
            {
                return Items == null ? 0 : Items.Min(x => x.DistanceInMeters);
            }
        }

        public double MaximumDistanceInMeters
        {
            get
            {
                return Items == null ? 0 : Items.Max(x => x.DistanceInMeters);
            }
        }

        public double TotalDistanceInMeters
        {
            get
            {
                return Items == null ? 0 : Items.Sum(x => x.DistanceInMeters);
            }
        }

        public TimeSpan AverageDuration
        {
            get
            {
                if (Items == null)
                    return TimeSpan.Zero;

                var durationInSec = Items.Average(x => x.Duration.TotalSeconds);
                return TimeSpan.FromSeconds(durationInSec);
            }
        }

        public TimeSpan MinimumDuration
        {
            get
            {
                if (Items == null)
                    return TimeSpan.Zero;

                var durationInSec = Items.Min(x => x.Duration.TotalSeconds);
                return TimeSpan.FromSeconds(durationInSec);
            }
        }

        public TimeSpan MaximumDuration
        {
            get
            {
                if (Items == null)
                    return TimeSpan.Zero;

                var durationInSec = Items.Max(x => x.Duration.TotalSeconds);
                return TimeSpan.FromSeconds(durationInSec);
            }
        }

        public TimeSpan TotalDuration
        {
            get
            {
                if (Items == null)
                    return TimeSpan.Zero;

                var durationInSec = Items.Sum(x => x.Duration.TotalSeconds);
                return TimeSpan.FromSeconds(durationInSec);
            }
        }

        public double AverageEnergyInKCal
        {
            get
            {
                return Items == null ? 0 : Items.Average(x => x.EnergyInKCal);
            }
        }

        public double MinimumEnergyInKCal
        {
            get
            {
                return Items == null ? 0 : Items.Min(x => x.EnergyInKCal);
            }
        }

        public double MaximumEnergyInKCal
        {
            get
            {
                return Items == null ? 0 : Items.Max(x => x.EnergyInKCal);
            }
        }

        public double TotalEnergyInKCal
        {
            get
            {
                return Items == null ? 0 : Items.Sum(x => x.EnergyInKCal);
            }
        }
    }
}
