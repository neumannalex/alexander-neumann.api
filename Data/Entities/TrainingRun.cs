using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace alexander_neumann.api.Data.Entities
{
    public class TrainingRun : IAuditableEntity
    {
        public string Id { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public string UserId { get; set; }
        public DateTime TrainingDate { get; set; }
        public TimeSpan Duration { get; set; }
        public double DistanceInMeters { get; set; }
        public double EnergyInKCal { get; set; }

        public int TrainingCalendarWeek
        {
            get
            {
                return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(TrainingDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            }
        }
        public TimeSpan AveragePacePerKilometer
        {
            get
            {
                if (DistanceInMeters <= 0)
                    return TimeSpan.MaxValue;

                var secondsPerMeter = Duration.TotalSeconds / DistanceInMeters;
                var secondsPerKilometer = 1000.0 * secondsPerMeter;

                return TimeSpan.FromSeconds(secondsPerKilometer);
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
