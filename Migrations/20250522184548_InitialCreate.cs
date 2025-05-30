using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientContact1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientContact2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientEmail1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientEmail2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSourceNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Client_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductsAlias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductsDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductsSellingPrice = table.Column<int>(type: "int", nullable: false),
                    ProductsCostingPrice = table.Column<int>(type: "int", nullable: false),
                    ProductsStatus = table.Column<bool>(type: "bit", nullable: false),
                    ProductsEntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductsModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductsId);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaymentsMade = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentIssue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FbFollowers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IgFollowers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GmbRakning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    BillingType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ProjectCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProjectCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignedUsers",
                columns: table => new
                {
                    AssignedUsersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedUsers", x => x.AssignedUsersId);
                    table.ForeignKey(
                        name: "FK_AssignedUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketingPhases",
                columns: table => new
                {
                    MarketingTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketingTypes = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingPhases", x => x.MarketingTaskId);
                    table.ForeignKey(
                        name: "FK_MarketingPhases_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentSchedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSchedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_PaymentSchedule_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BillingType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPost = table.Column<int>(type: "int", nullable: false),
                    TotalReels = table.Column<int>(type: "int", nullable: false),
                    AdsBudget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraField1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField3 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProjectsProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_Services_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "ProductsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "WebDevelopment",
                columns: table => new
                {
                    WebDevelopmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectIssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectDomainName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectHostingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectHostingRenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectHostingRenewalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProjectServerFtpAssign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectServerIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectServerUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectServerPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectHandledBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectDeadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectExtendedDeadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectIsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProjectRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectIssueBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebDevelopment", x => x.WebDevelopmentId);
                    table.ForeignKey(
                        name: "FK_WebDevelopment_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seo",
                columns: table => new
                {
                    SeoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketingTaskId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPosts = table.Column<int>(type: "int", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ranking = table.Column<int>(type: "int", nullable: true),
                    TotalFollowers = table.Column<int>(type: "int", nullable: true),
                    TotalLikes = table.Column<int>(type: "int", nullable: true),
                    EngagementRate = table.Column<double>(type: "float", nullable: true),
                    IssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seo", x => x.SeoId);
                    table.ForeignKey(
                        name: "FK_Seo_MarketingPhases_MarketingTaskId",
                        column: x => x.MarketingTaskId,
                        principalTable: "MarketingPhases",
                        principalColumn: "MarketingTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaHandling",
                columns: table => new
                {
                    SocialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketingTaskId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPosts = table.Column<int>(type: "int", nullable: true),
                    TotalFollowers = table.Column<int>(type: "int", nullable: true),
                    TotalLikes = table.Column<int>(type: "int", nullable: true),
                    EngagementRate = table.Column<double>(type: "float", nullable: true),
                    IssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaHandling", x => x.SocialId);
                    table.ForeignKey(
                        name: "FK_SocialMediaHandling_MarketingPhases_MarketingTaskId",
                        column: x => x.MarketingTaskId,
                        principalTable: "MarketingPhases",
                        principalColumn: "MarketingTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentSchedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "PaymentSchedule",
                        principalColumn: "ScheduleId");
                });

            migrationBuilder.CreateTable(
                name: "OthersService",
                columns: table => new
                {
                    OthersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    LableName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Post = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExtraField1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OthersService", x => x.OthersId);
                    table.ForeignKey(
                        name: "FK_OthersService_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeoServiceDetails",
                columns: table => new
                {
                    SeoServiceDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    KeyWord = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExtraField1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeoServiceDetails", x => x.SeoServiceDetailsId);
                    table.ForeignKey(
                        name: "FK_SeoServiceDetails_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkRecords",
                columns: table => new
                {
                    WorkRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Post = table.Column<int>(type: "int", nullable: false),
                    Reels = table.Column<int>(type: "int", nullable: false),
                    Ads = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExtraField1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField3 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField4 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField5 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField6 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField7 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRecords", x => x.WorkRecordId);
                    table.ForeignKey(
                        name: "FK_WorkRecords_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesigningPhases",
                columns: table => new
                {
                    DesignTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebDevelopmentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DesignTool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MockupLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedbackStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeadlineDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesigningPhases", x => x.DesignTaskId);
                    table.ForeignKey(
                        name: "FK_DesigningPhases_WebDevelopment_WebDevelopmentId",
                        column: x => x.WebDevelopmentId,
                        principalTable: "WebDevelopment",
                        principalColumn: "WebDevelopmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentPhases",
                columns: table => new
                {
                    DevelopmentTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebDevelopmentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ProgrammingLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeRepoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestStatus = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeadlineDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevelopmentPhases", x => x.DevelopmentTaskId);
                    table.ForeignKey(
                        name: "FK_DevelopmentPhases_WebDevelopment_WebDevelopmentId",
                        column: x => x.WebDevelopmentId,
                        principalTable: "WebDevelopment",
                        principalColumn: "WebDevelopmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenancePhase",
                columns: table => new
                {
                    MaintenanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebDevelopmentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IssueType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SystemAffected = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenancePhase", x => x.MaintenanceId);
                    table.ForeignKey(
                        name: "FK_MaintenancePhase_WebDevelopment_WebDevelopmentId",
                        column: x => x.WebDevelopmentId,
                        principalTable: "WebDevelopment",
                        principalColumn: "WebDevelopmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkRecordsSeoDetails",
                columns: table => new
                {
                    WorkRecordsSeoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkRecordId = table.Column<int>(type: "int", nullable: false),
                    KeyWord = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRecordsSeoDetails", x => x.WorkRecordsSeoId);
                    table.ForeignKey(
                        name: "FK_WorkRecordsSeoDetails_WorkRecords_WorkRecordId",
                        column: x => x.WorkRecordId,
                        principalTable: "WorkRecords",
                        principalColumn: "WorkRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClientId",
                table: "Accounts",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignedUsers_ProjectId",
                table: "AssignedUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedUsers_UserId",
                table: "AssignedUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                table: "Client",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DesigningPhases_WebDevelopmentId",
                table: "DesigningPhases",
                column: "WebDevelopmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPhases_WebDevelopmentId",
                table: "DevelopmentPhases",
                column: "WebDevelopmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenancePhase_WebDevelopmentId",
                table: "MaintenancePhase",
                column: "WebDevelopmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketingPhases_ProjectId",
                table: "MarketingPhases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_OthersService_ServiceId",
                table: "OthersService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_ProjectId",
                table: "PaymentSchedule",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seo_MarketingTaskId",
                table: "Seo",
                column: "MarketingTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeoServiceDetails_ServiceId",
                table: "SeoServiceDetails",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ProductsId",
                table: "Services",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ProjectId",
                table: "Services",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ProjectsProjectId",
                table: "Services",
                column: "ProjectsProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaHandling_MarketingTaskId",
                table: "SocialMediaHandling",
                column: "MarketingTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ScheduleId",
                table: "Transactions",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UsersUserId",
                table: "UserRoles",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WebDevelopment_ProjectId",
                table: "WebDevelopment",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkRecords_ServiceId",
                table: "WorkRecords",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRecordsSeoDetails_WorkRecordId",
                table: "WorkRecordsSeoDetails",
                column: "WorkRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedUsers");

            migrationBuilder.DropTable(
                name: "DesigningPhases");

            migrationBuilder.DropTable(
                name: "DevelopmentPhases");

            migrationBuilder.DropTable(
                name: "MaintenancePhase");

            migrationBuilder.DropTable(
                name: "OthersService");

            migrationBuilder.DropTable(
                name: "Seo");

            migrationBuilder.DropTable(
                name: "SeoServiceDetails");

            migrationBuilder.DropTable(
                name: "SocialMediaHandling");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "WorkRecordsSeoDetails");

            migrationBuilder.DropTable(
                name: "WebDevelopment");

            migrationBuilder.DropTable(
                name: "MarketingPhases");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "PaymentSchedule");

            migrationBuilder.DropTable(
                name: "WorkRecords");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
