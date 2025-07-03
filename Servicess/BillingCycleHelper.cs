using OfficeProject.Models.Enums;

namespace OfficeProject.Servicess
{
    public class BillingCycleHelper : IBillingCycleHelper
    {
        public (DateTime periodStart, DateTime periodEnd) GetCurrentBillingPeriod(DateTime serviceStartDate, BillingType billingType, DateTime today)
        {
            int monthsToAdd = billingType switch
            {
                BillingType.Monthly => 1,
                BillingType.Quarterly => 3,
                BillingType.HalfYearly => 6,
                BillingType.Yearly => 12,
                BillingType.OneTime => 0,
                _ => throw new ArgumentOutOfRangeException()
            };

            //if (billingType == BillingType.OneTime)
            //{
            //    if (billingType == BillingType.OneTime)
            //    {
            //        return (serviceStartDate, serviceStartDate.AddYears(1).AddDays(-1));
            //    }
            //}
            if (billingType == BillingType.OneTime)
            {
                if (billingType == BillingType.OneTime)
                {
                    return (serviceStartDate, DateTime.Now.Date);
                }
            }

            DateTime periodStart = serviceStartDate;
            while (periodStart.AddMonths(monthsToAdd) <= today)
            {
                periodStart = periodStart.AddMonths(monthsToAdd);
            }

            DateTime periodEnd = periodStart.AddMonths(monthsToAdd).AddDays(-1);
            return (periodStart, periodEnd);
        }

        public bool IsBillingExpired(DateTime serviceStartDate, BillingType billingType, DateTime today)
        {
            //if (billingType == BillingType.OneTime)
            //{
            //    return today.Date > serviceStartDate.Date;
            //}

            //var (_, periodEnd) = GetCurrentBillingPeriod(serviceStartDate, billingType, today);
            //return today.Date > periodEnd.Date;
      
            var (start, end) = GetCurrentBillingPeriod(serviceStartDate, billingType, today);
            return today > end;
        }

    }
}


