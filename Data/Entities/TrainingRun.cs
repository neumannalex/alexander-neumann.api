using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alexander_neumann.api.Data.Entities
{
    public class TrainingRun
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public DateTime TrainingDate { get; set; }
        public TimeSpan Duration { get; set; }
        public double DistanceInMeters { get; set; }
        public double EnergyInKCal { get; set; }

        public double AveragePaceInMinutesPerKilometer
        {
            get
            {
                if (DistanceInMeters <= 0)
                    return double.PositiveInfinity;

                var secondsPerMeter = Duration.TotalSeconds / DistanceInMeters;
                var minutesPerKilometer = 1000.0 * secondsPerMeter / 60.0;

                return minutesPerKilometer;
            }
        }
        public double AverageSpeedInKilometersPerHour
        {
            get
            {
                if (Duration <= TimeSpan.Zero)
                    return double.PositiveInfinity;

                var metersPerSecond = DistanceInMeters / Duration.TotalSeconds;

                return metersPerSecond * 3.6;
            }
        }
    }
}
