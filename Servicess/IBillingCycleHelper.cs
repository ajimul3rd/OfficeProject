using OfficeProject.Models.Enums;

namespace OfficeProject.Servicess
{
    public interface IBillingCycleHelper
    {
        (DateTime periodStart, DateTime periodEnd) GetCurrentBillingPeriod(DateTime serviceStartDate, BillingType billingType, DateTime today);
        bool IsBillingExpired(DateTime serviceStartDate, BillingType billingType, DateTime today);
    }
}
