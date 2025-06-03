using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeProject.Migrations
{
    /// <inheritdoc />
    public partial class AddWebdevelopmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentSchedule_Projects_ProjectId",
                table: "PaymentSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_WebDevelopment_Projects_ProjectId",
                table: "WebDevelopment");

            migrationBuilder.DropTable(
                name: "DesigningPhases");

            migrationBuilder.DropTable(
                name: "DevelopmentPhases");

            migrationBuilder.DropTable(
                name: "MaintenancePhase");

            migrationBuilder.DropTable(
                name: "Seo");

            migrationBuilder.DropTable(
                name: "SocialMediaHandling");

            migrationBuilder.DropTable(
                name: "MarketingPhases");

            migrationBuilder.DropIndex(
                name: "IX_WebDevelopment_ProjectId",
                table: "WebDevelopment");

            migrationBuilder.DropColumn(
                name: "ProjectDeadline",
                table: "WebDevelopment");

            migrationBuilder.DropColumn(
                name: "ProjectHostingRenewalAmount",
                table: "WebDevelopment");

            migrationBuilder.RenameColumn(
                name: "ProjectStartDate",
                table: "WebDevelopment",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ProjectServerUserId",
                table: "WebDevelopment",
                newName: "ServerUserId");

            migrationBuilder.RenameColumn(
                name: "ProjectServerPassword",
                table: "WebDevelopment",
                newName: "ServerPassword");

            migrationBuilder.RenameColumn(
                name: "ProjectServerIp",
                table: "WebDevelopment",
                newName: "ServerIp");

            migrationBuilder.RenameColumn(
                name: "ProjectServerFtpAssign",
                table: "WebDevelopment",
                newName: "ServerFtpAssign");

            migrationBuilder.RenameColumn(
                name: "ProjectRemarks",
                table: "WebDevelopment",
                newName: "Remarks");

            migrationBuilder.RenameColumn(
                name: "ProjectIssueDate",
                table: "WebDevelopment",
                newName: "IssueDate");

            migrationBuilder.RenameColumn(
                name: "ProjectIssueBy",
                table: "WebDevelopment",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "ProjectIsActive",
                table: "WebDevelopment",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "ProjectHostingRenewalDate",
                table: "WebDevelopment",
                newName: "HostingRenewalDate");

            migrationBuilder.RenameColumn(
                name: "ProjectHostingDate",
                table: "WebDevelopment",
                newName: "HostingDate");

            migrationBuilder.RenameColumn(
                name: "ProjectHandledBy",
                table: "WebDevelopment",
                newName: "MackupLink");

            migrationBuilder.RenameColumn(
                name: "ProjectExtendedDeadline",
                table: "WebDevelopment",
                newName: "Deadline");

            migrationBuilder.RenameColumn(
                name: "ProjectDomainName",
                table: "WebDevelopment",
                newName: "Languages");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "PaymentSchedule",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentSchedule_ProjectId",
                table: "PaymentSchedule",
                newName: "IX_PaymentSchedule_ServiceId");

            migrationBuilder.AddColumn<string>(
                name: "DesignTools",
                table: "WebDevelopment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DomainName",
                table: "WebDevelopment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HostingRenewalAmount",
                table: "WebDevelopment",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectsProjectId",
                table: "PaymentSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebDevelopment_ProjectId",
                table: "WebDevelopment",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_ProjectsProjectId",
                table: "PaymentSchedule",
                column: "ProjectsProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentSchedule_Projects_ProjectsProjectId",
                table: "PaymentSchedule",
                column: "ProjectsProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentSchedule_Services_ServiceId",
                table: "PaymentSchedule",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebDevelopment_Services_ProjectId",
                table: "WebDevelopment",
                column: "ProjectId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentSchedule_Projects_ProjectsProjectId",
                table: "PaymentSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentSchedule_Services_ServiceId",
                table: "PaymentSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_WebDevelopment_Services_ProjectId",
                table: "WebDevelopment");

            migrationBuilder.DropIndex(
                name: "IX_WebDevelopment_ProjectId",
                table: "WebDevelopment");

            migrationBuilder.DropIndex(
                name: "IX_PaymentSchedule_ProjectsProjectId",
                table: "PaymentSchedule");

            migrationBuilder.DropColumn(
                name: "DesignTools",
                table: "WebDevelopment");

            migrationBuilder.DropColumn(
                name: "DomainName",
                table: "WebDevelopment");

            migrationBuilder.DropColumn(
                name: "HostingRenewalAmount",
                table: "WebDevelopment");

            migrationBuilder.DropColumn(
                name: "ProjectsProjectId",
                table: "PaymentSchedule");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "WebDevelopment",
                newName: "ProjectStartDate");

            migrationBuilder.RenameColumn(
                name: "ServerUserId",
                table: "WebDevelopment",
                newName: "ProjectServerUserId");

            migrationBuilder.RenameColumn(
                name: "ServerPassword",
                table: "WebDevelopment",
                newName: "ProjectServerPassword");

            migrationBuilder.RenameColumn(
                name: "ServerIp",
                table: "WebDevelopment",
                newName: "ProjectServerIp");

            migrationBuilder.RenameColumn(
                name: "ServerFtpAssign",
                table: "WebDevelopment",
                newName: "ProjectServerFtpAssign");

            migrationBuilder.RenameColumn(
                name: "Remarks",
                table: "WebDevelopment",
                newName: "ProjectRemarks");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "WebDevelopment",
                newName: "ProjectIssueBy");

            migrationBuilder.RenameColumn(
                name: "MackupLink",
                table: "WebDevelopment",
                newName: "ProjectHandledBy");

            migrationBuilder.RenameColumn(
                name: "Languages",
                table: "WebDevelopment",
                newName: "ProjectDomainName");

            migrationBuilder.RenameColumn(
                name: "IssueDate",
                table: "WebDevelopment",
                newName: "ProjectIssueDate");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "WebDevelopment",
                newName: "ProjectIsActive");

            migrationBuilder.RenameColumn(
                name: "HostingRenewalDate",
                table: "WebDevelopment",
                newName: "ProjectHostingRenewalDate");

            migrationBuilder.RenameColumn(
                name: "HostingDate",
                table: "WebDevelopment",
                newName: "ProjectHostingDate");

            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "WebDevelopment",
                newName: "ProjectExtendedDeadline");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "PaymentSchedule",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentSchedule_ServiceId",
                table: "PaymentSchedule",
                newName: "IX_PaymentSchedule_ProjectId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProjectDeadline",
                table: "WebDevelopment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectHostingRenewalAmount",
                table: "WebDevelopment",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "DesigningPhases",
                columns: table => new
                {
                    DesignTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebDevelopmentId = table.Column<int>(type: "int", nullable: false),
                    DeadlineDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignTool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FeedbackStatus = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    MockupLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CodeRepoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeadlineDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProgrammingLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TestStatus = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SystemAffected = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "MarketingPhases",
                columns: table => new
                {
                    MarketingTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketingTypes = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Seo",
                columns: table => new
                {
                    SeoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketingTaskId = table.Column<int>(type: "int", nullable: false),
                    EngagementRate = table.Column<double>(type: "float", nullable: true),
                    IssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ranking = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalFollowers = table.Column<int>(type: "int", nullable: true),
                    TotalLikes = table.Column<int>(type: "int", nullable: true),
                    TotalPosts = table.Column<int>(type: "int", nullable: false)
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
                    EngagementRate = table.Column<double>(type: "float", nullable: true),
                    IssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalFollowers = table.Column<int>(type: "int", nullable: true),
                    TotalLikes = table.Column<int>(type: "int", nullable: true),
                    TotalPosts = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_WebDevelopment_ProjectId",
                table: "WebDevelopment",
                column: "ProjectId",
                unique: true);

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
                name: "IX_Seo_MarketingTaskId",
                table: "Seo",
                column: "MarketingTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaHandling_MarketingTaskId",
                table: "SocialMediaHandling",
                column: "MarketingTaskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentSchedule_Projects_ProjectId",
                table: "PaymentSchedule",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WebDevelopment_Projects_ProjectId",
                table: "WebDevelopment",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
