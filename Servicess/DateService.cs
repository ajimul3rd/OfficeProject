using OfficeProject.Models.Enums;

namespace OfficeProject.Servicess
{

    public class DateService
    {
        public int CalculateWorkingDays(DateTime startDate, BillingType periodType)
        {
            DateTime endDate = periodType switch
            {
                BillingType.Monthly => startDate.AddMonths(1).AddDays(-1),
                BillingType.Quarterly => startDate.AddMonths(3).AddDays(-1),
                BillingType.HalfYearly => startDate.AddMonths(6).AddDays(-1),
                BillingType.Yearly => startDate.AddYears(1),
                BillingType.OneTime => startDate, // single day
                _ => throw new ArgumentOutOfRangeException(nameof(periodType), "Unsupported period type")
            };

            int workingDays = 0;
            DateTime currentDate = startDate;
            while (currentDate <= endDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
                currentDate = currentDate.AddDays(1);
            }

            return workingDays;
        }

        public List<DateTime> GetBreakPointDates(DateTime startDate, BillingType periodType, int totalUnits)
        {
            int workingDaysInPeriod = CalculateWorkingDays(startDate, periodType);

            List<DateTime> breakPoints = new();
            double daysPerUnit = (double)workingDaysInPeriod / totalUnits;
            int workingDayCounter = 0;
            int unitIndex = 1;
            DateTime currentDate = startDate;
            breakPoints.Add(currentDate);
            while (unitIndex <= totalUnits)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDayCounter++;
                    double expectedWorkingDayForUnit = daysPerUnit * unitIndex;
                    if (workingDayCounter >= expectedWorkingDayForUnit)
                    {
                        breakPoints.Add(currentDate);
                        unitIndex++;
                    }
                }
                currentDate = currentDate.AddDays(1);
            }

            return breakPoints;
        }

        public int GetTotalDaysExcludingSundays(DateTime startDate, int totalDays)
        {
            int workingDays = 0;

            for (int i = 0; i < totalDays; i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                if (currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
            }

            return workingDays;
        }

        public int GetIntervalDaysExcludingSundays(DateTime startDate)
        {
            int workingDays = 0;
            DateTime currentDate = DateTime.Today;

            int totalDays = (currentDate - startDate).Days + 1; // inclusive of today

            for (int i = 0; i < totalDays; i++)
            {
                DateTime dateToCheck = startDate.AddDays(i);
                if (dateToCheck.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
            }

            return workingDays;
        }



        public (DateTime Start, DateTime End)? GetCurrentInterval(List<DateTime> breakpoints)
        {
            DateTime currentDate = DateTime.Today;

            for (int i = 0; i < breakpoints.Count - 1; i++)
            {
                if (currentDate >= breakpoints[i] && currentDate <= breakpoints[i + 1])
                {
                    return (breakpoints[i], breakpoints[i + 1]);
                }
            }
            return null; // not found
        }
        //public (DateTime Start, DateTime End)? GetPriviousInterval(List<DateTime> breakpoints)
        //{
        //    DateTime currentDate = DateTime.Today;

        //    for (int i = 0; i < breakpoints.Count - 1; i++)
        //    {
        //        if (currentDate >= breakpoints[i] && currentDate <= breakpoints[i + 1])
        //        {
        //            return (breakpoints[i], breakpoints[i + 1]);
        //        }
        //    }
        //    return null; // not found
        //}
    }
} 