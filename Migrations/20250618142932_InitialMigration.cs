using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientSources",
                columns: table => new
                {
                    ClientSourcesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientSourcesName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientSources", x => x.ClientSourcesId);
                });

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
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkingActivityList",
                columns: table => new
                {
                    MasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkingActivityList", x => x.MasterId);
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
                name: "UserDesignation",
                columns: table => new
                {
                    DesignationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDesignation", x => x.DesignationId);
                    table.ForeignKey(
                        name: "FK_UserDesignation_Users_User_Id",
                        column: x => x.User_Id,
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
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPaymentsMade = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
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
                    ProjectCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
                name: "UserWorkingActivity",
                columns: table => new
                {
                    workingActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    WorkingActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkingActivity", x => x.workingActivityId);
                    table.ForeignKey(
                        name: "FK_UserWorkingActivity_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "ProductsId",
                        onDelete: ReferentialAction.Cascade);
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPost = table.Column<int>(type: "int", nullable: false),
                    TotalReels = table.Column<int>(type: "int", nullable: false),
                    AdsBudget = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraField1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExtraField3 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_Services_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "ProductsId");
                    table.ForeignKey(
                        name: "FK_Services_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
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
                name: "PaymentSchedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectsProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSchedule", x => x.ScheduleId);
                    
                    table.ForeignKey(
                        name: "FK_PaymentSchedule_Services_ServiceId",
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
                name: "WebDevelopment",
                columns: table => new
                {
                    WebDevelopmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    DomainName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HostingRenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HostingLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostingRenewalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ServerFtpAssign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServerIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServerUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServerPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignTools = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MackupLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebDevelopment", x => x.WebDevelopmentId);
                    table.ForeignKey(
                        name: "FK_WebDevelopment_Services_ServiceId",
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
                    Work_UserId = table.Column<int>(type: "int", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SharedPost = table.Column<int>(type: "int", nullable: false),
                    CreatedReels = table.Column<int>(type: "int", nullable: false),
                    UsedAdsBudget = table.Column<int>(type: "int", nullable: false),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_WorkRecords_Users_Work_UserId",
                        column: x => x.Work_UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
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
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
                name: "OthersTaskDetails",
                columns: table => new
                {
                    OthersTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkRecordId = table.Column<int>(type: "int", nullable: false),
                    LableName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SharedPost = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OthersTaskDetails", x => x.OthersTaskId);
                    table.ForeignKey(
                        name: "FK_OthersTaskDetails_WorkRecords_WorkRecordId",
                        column: x => x.WorkRecordId,
                        principalTable: "WorkRecords",
                        principalColumn: "WorkRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeoTaskDetails",
                columns: table => new
                {
                    SeoTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkRecordId = table.Column<int>(type: "int", nullable: false),
                    KeyWord = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CurrentRank = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeoTaskDetails", x => x.SeoTaskId);
                    table.ForeignKey(
                        name: "FK_SeoTaskDetails_WorkRecords_WorkRecordId",
                        column: x => x.WorkRecordId,
                        principalTable: "WorkRecords",
                        principalColumn: "WorkRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebDeveTaskDetails",
                columns: table => new
                {
                    webDevTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkRecordId = table.Column<int>(type: "int", nullable: false),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebDeveTaskDetails", x => x.webDevTaskId);
                    table.ForeignKey(
                        name: "FK_WebDeveTaskDetails_WorkRecords_WorkRecordId",
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
                name: "IX_OthersService_ServiceId",
                table: "OthersService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OthersTaskDetails_WorkRecordId",
                table: "OthersTaskDetails",
                column: "WorkRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_ProjectsProjectId",
                table: "PaymentSchedule",
                column: "ProjectsProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_ServiceId",
                table: "PaymentSchedule",
                column: "ServiceId");

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
                name: "IX_SeoServiceDetails_ServiceId",
                table: "SeoServiceDetails",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SeoTaskDetails_WorkRecordId",
                table: "SeoTaskDetails",
                column: "WorkRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ProductsId",
                table: "Services",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ProjectId",
                table: "Services",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ScheduleId",
                table: "Transactions",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDesignation_User_Id",
                table: "UserDesignation",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UsersUserId",
                table: "UserRoles",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkingActivity_ProductsId",
                table: "UserWorkingActivity",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_WebDevelopment_ServiceId",
                table: "WebDevelopment",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_WebDeveTaskDetails_WorkRecordId",
                table: "WebDeveTaskDetails",
                column: "WorkRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRecords_ServiceId",
                table: "WorkRecords",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRecords_Work_UserId",
                table: "WorkRecords",
                column: "Work_UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedUsers");

            migrationBuilder.DropTable(
                name: "clientSources");

            migrationBuilder.DropTable(
                name: "OthersService");

            migrationBuilder.DropTable(
                name: "OthersTaskDetails");

            migrationBuilder.DropTable(
                name: "SeoServiceDetails");

            migrationBuilder.DropTable(
                name: "SeoTaskDetails");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserDesignation");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserWorkingActivity");

            migrationBuilder.DropTable(
                name: "UserWorkingActivityList");

            migrationBuilder.DropTable(
                name: "WebDevelopment");

            migrationBuilder.DropTable(
                name: "WebDeveTaskDetails");

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
