namespace OfficeProject.Models.Enums
{

    public enum UserRole
    {
        ADMIN,
        MANAGER,
        USER
    }
    public enum PaymentStatus
    {
        PENDING,
        PARTIAL,
        COMPLETED,
        OVERDUE,
        REFUNDED
    }
    public enum BillingType
    {
        Monthly,
        Quarterly,
        HalfYearly,
        Yearly,
        OneTime
    }


    public enum ProjectType
    {
        SERVICE,
        EXTRA_SERVICE,
    }

    public enum ProjectPhaseType
    {
        DESIGNING,      
        DEVELOPMENT,    
        MAINTENANCE     
    }

    public enum MaintenanceStatus
    {
        REPORTED,
        IN_PROGRESS,
        FIXED,
        CLOSED,
        REOPENED
    }

    public enum MaintenancePriority
    {
        LOW,
        MEDIUM,
        HIGH,
        CRITICAL
    }

    public enum IssueType
    {
        GENERAL,
        BUG,
        PERFORMANCE,
        SECURITY,
        UI,
        INTEGRATION
    }

    public enum DevelopmentStatus
    {
        ASSIGNED,
        IN_PROGRESS,
        CODE_REVIEW,
        COMPLETED,
        BLOCKED
    }

    public enum TestingStatus
    {
        NOT_TESTED,
        PASSED,
        FAILED,
        IN_PROGRESS,
        NEEDS_FIX
    }

    public enum DesignStatus
    {
        ASSIGNED,
        IN_PROGRESS,
        CLIENT_REVIEW,
        REVISING,
        APPROVED,
        REJECTED
    }

    public enum FeedbackStatus
    {
        PENDING,
        POSITIVE,
        NEGATIVE,
        NEEDS_REVISION,
        APPROVED_WITH_COMMENTS
    }

    public enum MarketingStatus
    {
        PLANNED,
        ACTIVE,
        COMPLETED
    }

    public enum MarketingTypes
    {
        SOCIAL_MEDIA_HANDLING,
        SEO,
    }

    public enum MarketingPlatform
    {
        NOT_DEFINED,
        FACEBOOK,
        GOOGLE_ADS,
        INSTAGRAM,
        TWITTER,
        LINKEDIN
    }
}
