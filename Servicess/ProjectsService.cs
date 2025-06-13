using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace OfficeProject.Servicess
{
    public class ProjectsService : IProjectsService
    {

        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        public ProjectsService(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;

        }

        public async Task AddProjectsAsync(ProjectsDTO projectsDto)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                if (projectsDto != null)
                {
                    var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                    {
                        throw new UnauthorizedAccessException("User is not authenticated.");
                    }

                    var data = Mapper.Map<Projects>(projectsDto);
                    data.UserId = userId;  // Assign logged-in user's UserId here

                    await context.Projects.AddAsync(data);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<ProjectsDTO?>> GetProjectPerUserAsync()
        {
            try
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }

                using (var context = dbContextFactory.CreateDbContext())
                {
                    // Get user with designation
                    var user = await context.Users
                        .Include(u => u.UserDesignation)
                        .FirstOrDefaultAsync(u => u.UserId == userId);

                    if (user == null)
                    {
                        throw new UnauthorizedAccessException("User not found.");
                    }

                    //var userDesignations = user.UserDesignation?? new List<UserDesignation>();
                    var designationNames = user.UserDesignation?.Select(d => d.Designation).ToHashSet() ?? new HashSet<string>();



                    var projects = await context.Projects
                        .Include(p => p.Client)
                        .Include(p => p.AssignedUsers)
                        .Include(p => p.Services)
                            .ThenInclude(s => s.Products)
                                .ThenInclude(p => p.UserWorkingActivity)
                        .Include(p => p.Services)
                            .ThenInclude(s => s.SeoServiceDetails)
                        .Include(p => p.Services)
                            .ThenInclude(s => s.OthersServices)
                        .Include(p => p.Services)
                            .ThenInclude(s => s.WebDevelopment)
                        .Where(p => p.AssignedUsers.Any(u => u.UserId == userId))
                        .ToListAsync();

                    var projectDTOs = projects.Select(project => new ProjectsDTO
                    {
                        ProjectId = project.ProjectId,
                        UserId = project.UserId,
                        ClientId = project.ClientId,
                        ProjectName = project.ProjectName,
                        BillingType = project.BillingType,
                        ProjectStartDate = project.ProjectStartDate,
                        ProjectType = project.ProjectType,
                        ProjectCost = project.ProjectCost,
                        CurrentIssue = project.CurrentIssue,
                        InternalRemark = project.InternalRemark,
                        CustomerNote = project.CustomerNote,
                        FbFollowers = project.FbFollowers,
                        IgFollowers = project.IgFollowers,
                        GmbRakning = project.GmbRakning,
                        IsActive = project.IsActive,

                        AssignedUsers = project.AssignedUsers?.Select(user => new AssignedUsersDTO
                        {
                            AssignedUsersId = user.AssignedUsersId,
                            ProjectId = user.ProjectId,
                            UserId = user.UserId,
                            UserName = user.UserName,
                            Role = user.Role
                        }).ToList(),

                        Client = project.Client != null ? new ClientsDTO
                        {
                            ClientId = project.Client.ClientId,
                            ClientName = project.Client.ClientName,
                            ClientEmail1 = project.Client.ClientEmail1,
                            ClientContact1 = project.Client.ClientContact1
                        } : null,

                        Services = project.Services
                            .Where(service =>
                                service.Products?.UserWorkingActivity != null &&
                                service.Products.UserWorkingActivity.Any(ua =>
                                designationNames.Contains(ua.WorkingActivityName)))
                            .Select(service => new ServicesDTO
                            {
                                ServiceId = service.ServiceId,
                                ProjectId = service.ProjectId,
                                ProductsId = service.ProductsId,
                                ServiceName = service.ServiceName,
                                BillingType = service.BillingType,
                                Price = service.Price,
                                FinalPrice = service.FinalPrice,
                                StartDate = service.StartDate,
                                EndDate = service.EndDate,
                                TotalPost = service.TotalPost,
                                TotalReels = service.TotalReels,
                                AdsBudget = service.AdsBudget,
                                DeadLine = service.DeadLine,
                                ExtraField1 = service.ExtraField1,
                                ExtraField2 = service.ExtraField2,
                                ExtraField3 = service.ExtraField3,

                                SeoServiceDetails = service.SeoServiceDetails?.Select(detail => new SeoServiceDetailsDTO
                                {
                                    SeoServiceDetailsId = detail.SeoServiceDetailsId,
                                    ServiceId = detail.ServiceId,
                                    KeyWord = detail.KeyWord,
                                    Rank = detail.Rank,
                                    Note = detail.Note,
                                    ExtraField1 = detail.ExtraField1,
                                    ExtraField2 = detail.ExtraField2
                                }).ToList(),

                                OthersServices = service.OthersServices?.Select(other => new OthersServiceDTO
                                {
                                    OthersId = other.OthersId,
                                    ServiceId = other.ServiceId,
                                    LableName = other.LableName,
                                    Post = other.Post,
                                    Note = other.Note,
                                    ExtraField1 = other.ExtraField1,
                                    ExtraField2 = other.ExtraField2
                                }).ToList(),

                                WebDevelopment = service.WebDevelopment?.Select(web => new WebDevelopmentDTO
                                {
                                    WebDevelopmentId = web.WebDevelopmentId,
                                    ServiceId = web.ServiceId,
                                    DomainName = web.DomainName,
                                    HostingDate = web.HostingDate,
                                    HostingRenewalDate = web.HostingRenewalDate,
                                    HostingLimit = web.HostingLimit,
                                    HostingRenewalAmount = web.HostingRenewalAmount,
                                    ServerFtpAssign = web.ServerFtpAssign,
                                    ServerIp = web.ServerIp,
                                    ServerUserId = web.ServerUserId,
                                    ServerPassword = web.ServerPassword,
                                    DesignTools = web.DesignTools,
                                    MackupLink = web.MackupLink,
                                    Languages = web.Languages,
                                    IsActive = web.IsActive,
                                    StartDate = web.StartDate,
                                    Deadline = web.Deadline,
                                    Remarks = web.Remarks,
                                    Note = web.Note
                                }).ToList()
                            }).ToList()

                    }).Where(p => p.Services != null && p.Services.Any()).ToList(); // Filter out projects with no matching services
                    //string json = JsonSerializer.Serialize(projectDTOs, new JsonSerializerOptions
                    //{
                    //    WriteIndented = true
                    //});
                    //Console.WriteLine($"GetProjectPerUserAsync: {json}");
                    return projectDTOs;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving projects: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ProjectsDTO>> GetWorkingRecordPerUserAsync()
        {
            try
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }

                using (var context = dbContextFactory.CreateDbContext())
                {
                    // Get user with designation
                    var user = await context.Users
                        .Include(u => u.UserDesignation)
                        .FirstOrDefaultAsync(u => u.UserId == userId);

                    if (user == null)
                    {
                        throw new UnauthorizedAccessException("User not found.");
                    }

                    var designationNames = user.UserDesignation?.Select(d => d.Designation).ToHashSet() ?? new HashSet<string>();

                    var projects = await context.Projects
                        .Include(p => p.Client)
                        .Include(p => p.AssignedUsers)
                        .Include(p => p.Services)
                            .ThenInclude(s => s.Products)
                                .ThenInclude(p => p.UserWorkingActivity)
                        .Include(p => p.Services)
                            .ThenInclude(w => w.WorkRecords)
                        .Include(p => p.Services)
                            .ThenInclude(s => s.SeoServiceDetails)
                        .Include(p => p.Services)
                            .ThenInclude(s => s.OthersServices)
                        .Include(p => p.Services)
                            .ThenInclude(s => s.WebDevelopment)
                        .Where(p => p.AssignedUsers.Any(u => u.UserId == userId))
                        .ToListAsync();

                    var projectDTOs = projects.Select(project => new ProjectsDTO
                    {
                        ProjectId = project.ProjectId,
                        UserId = project.UserId,
                        ClientId = project.ClientId,
                        ProjectName = project.ProjectName,
                        BillingType = project.BillingType,
                        ProjectStartDate = project.ProjectStartDate,
                        ProjectType = project.ProjectType,
                        ProjectCost = project.ProjectCost,
                        CurrentIssue = project.CurrentIssue,
                        InternalRemark = project.InternalRemark,
                        CustomerNote = project.CustomerNote,
                        FbFollowers = project.FbFollowers,
                        IgFollowers = project.IgFollowers,
                        GmbRakning = project.GmbRakning,
                        IsActive = project.IsActive,

                        AssignedUsers = project.AssignedUsers?.Select(user => new AssignedUsersDTO
                        {
                            AssignedUsersId = user.AssignedUsersId,
                            ProjectId = user.ProjectId,
                            UserId = user.UserId,
                            UserName = user.UserName,
                            Role = user.Role
                        }).ToList(),

                        Client = project.Client != null ? new ClientsDTO
                        {
                            ClientId = project.Client.ClientId,
                            ClientName = project.Client.ClientName,
                            ClientEmail1 = project.Client.ClientEmail1,
                            ClientContact1 = project.Client.ClientContact1
                        } : null,

                        Services = project.Services
                            .Where(service =>
                                (service.Products?.UserWorkingActivity != null &&
                                 service.Products.UserWorkingActivity.Any(ua =>
                                     designationNames.Contains(ua.WorkingActivityName))) ||
                                (service.WorkRecords != null && service.WorkRecords.Any()))
                            .Select(service => new ServicesDTO
                            {
                                ServiceId = service.ServiceId,
                                ProjectId = service.ProjectId,
                                ProductsId = service.ProductsId,
                                ServiceName = service.ServiceName,
                                BillingType = service.BillingType,
                                Price = service.Price,
                                FinalPrice = service.FinalPrice,
                                StartDate = service.StartDate,
                                EndDate = service.EndDate,
                                TotalPost = service.TotalPost,
                                TotalReels = service.TotalReels,
                                AdsBudget = service.AdsBudget,
                                DeadLine = service.DeadLine,
                                ExtraField1 = service.ExtraField1,
                                ExtraField2 = service.ExtraField2,
                                ExtraField3 = service.ExtraField3,

                                SeoServiceDetails = service.SeoServiceDetails?.Select(detail => new SeoServiceDetailsDTO
                                {
                                    SeoServiceDetailsId = detail.SeoServiceDetailsId,
                                    ServiceId = detail.ServiceId,
                                    KeyWord = detail.KeyWord,
                                    Rank = detail.Rank,
                                    Note = detail.Note,
                                    ExtraField1 = detail.ExtraField1,
                                    ExtraField2 = detail.ExtraField2
                                }).ToList(),

                                OthersServices = service.OthersServices?.Select(other => new OthersServiceDTO
                                {
                                    OthersId = other.OthersId,
                                    ServiceId = other.ServiceId,
                                    LableName = other.LableName,
                                    Post = other.Post,
                                    Note = other.Note,
                                    ExtraField1 = other.ExtraField1,
                                    ExtraField2 = other.ExtraField2
                                }).ToList(),

                                WebDevelopment = service.WebDevelopment?.Select(web => new WebDevelopmentDTO
                                {
                                    WebDevelopmentId = web.WebDevelopmentId,
                                    ServiceId = web.ServiceId,
                                    DomainName = web.DomainName,
                                    HostingDate = web.HostingDate,
                                    HostingRenewalDate = web.HostingRenewalDate,
                                    HostingLimit = web.HostingLimit,
                                    HostingRenewalAmount = web.HostingRenewalAmount,
                                    ServerFtpAssign = web.ServerFtpAssign,
                                    ServerIp = web.ServerIp,
                                    ServerUserId = web.ServerUserId,
                                    ServerPassword = web.ServerPassword,
                                    DesignTools = web.DesignTools,
                                    MackupLink = web.MackupLink,
                                    Languages = web.Languages,
                                    IsActive = web.IsActive,
                                    StartDate = web.StartDate,
                                    Deadline = web.Deadline,
                                    Remarks = web.Remarks,
                                    Note = web.Note
                                }).ToList(),

                                WorkRecords = service.WorkRecords?.Select(record => new WorkingRecordsDto
                                {
                                    WorkRecordId = record.WorkRecordId,
                                    ServiceId = record.ServiceId,
                                    WorkDate = record.WorkDate,
                                    //ServiceName = record.ServiceName,
                                    SharedPost = record.SharedPost,
                                    CreatedReels = record.CreatedReels,
                                    UsedAdsBudget = record.UsedAdsBudget,
                                    Task = record.Task,
                                    Status = record.Status,
                                    Remarks = record.Remarks,
                                    ExtraField1 = record.ExtraField1,
                                    ExtraField2 = record.ExtraField2,
                                    ExtraField3 = record.ExtraField3,
                                    ExtraField4 = record.ExtraField4,
                                    ExtraField5 = record.ExtraField5,
                                    ExtraField6 = record.ExtraField6,
                                    ExtraField7 = record.ExtraField7,
                                }).ToList()
                            }).ToList()

                    }).Where(p => p.Services != null && p.Services.Any()).ToList();

                    return projectDTOs;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving projects: {ex.Message}");
                throw;
            }
        }


        //public async Task<List<ProjectsDTO?>> GetProjectPerUserAsync()
        //{
        //    try
        //    {
        //        var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        //        {
        //            throw new UnauthorizedAccessException("User is not authenticated.");
        //        }

        //        using (var context = dbContextFactory.CreateDbContext())
        //        {
        //            var projects = await context.Projects
        //                .Include(p => p.Client)
        //                .Include(p => p.AssignedUsers)
        //                .Include(p => p.Services)
        //                    .ThenInclude(s => s.SeoServiceDetails)
        //                .Include(p => p.Services)
        //                    .ThenInclude(s => s.OthersServices)
        //                .Where(p => p.AssignedUsers.Any(u => u.UserId == userId))
        //                .ToListAsync();

        //            var projectDTOs = projects.Select(project => new ProjectsDTO
        //            {
        //                ProjectId = project.ProjectId,
        //                UserId = project.UserId,
        //                ClientId = project.ClientId,
        //                ProjectName = project.ProjectName,
        //                BillingType = project.BillingType,
        //                ProjectStartDate = project.ProjectStartDate,
        //                ProjectType = project.ProjectType,
        //                ProjectCost = project.ProjectCost,
        //                CurrentIssue = project.CurrentIssue,
        //                InternalRemark = project.InternalRemark,
        //                CustomerNote = project.CustomerNote,
        //                FbFollowers = project.FbFollowers,
        //                IgFollowers = project.IgFollowers,
        //                GmbRakning = project.GmbRakning,
        //                IsActive = project.IsActive,

        //                AssignedUsers = project.AssignedUsers?.Select(user => new AssignedUsersDTO
        //                {
        //                    AssignedUsersId = user.AssignedUsersId,
        //                    ProjectId = user.ProjectId,
        //                    UserId = user.UserId,
        //                    UserName = user.UserName,
        //                    Role = user.Role
        //                }).ToList(),

        //                Client = project.Client != null ? new ClientsDTO
        //                {
        //                    ClientId = project.Client.ClientId,
        //                    ClientName = project.Client.ClientName,
        //                    ClientEmail1 = project.Client.ClientEmail1,
        //                    ClientContact1 = project.Client.ClientContact1
        //                } : null,

        //                Services = project.Services.Select(service => new ServicesDTO
        //                {
        //                    ServiceId = service.ServiceId,
        //                    ProjectId = service.ProjectId,
        //                    ProductsId = service.ProductsId, // ✅ Mapped correctly
        //                    ServiceName = service.ServiceName,
        //                    BillingType = service.BillingType,
        //                    Price = service.Price,
        //                    FinalPrice = service.FinalPrice,
        //                    StartDate = service.StartDate,
        //                    EndDate = service.EndDate,
        //                    TotalPost = service.TotalPost,
        //                    TotalReels = service.TotalReels,
        //                    AdsBudget = service.AdsBudget,
        //                    DeadLine = service.DeadLine,
        //                    ExtraField1 = service.ExtraField1,
        //                    ExtraField2 = service.ExtraField2,
        //                    ExtraField3 = service.ExtraField3,

        //                    SeoServiceDetails = service.SeoServiceDetails?.Select(detail => new SeoServiceDetailsDTO
        //                    {
        //                        SeoServiceDetailsId = detail.SeoServiceDetailsId,
        //                        ServiceId = detail.ServiceId,
        //                        KeyWord = detail.KeyWord,
        //                        Rank = detail.Rank,
        //                        Note = detail.Note,
        //                        ExtraField1 = detail.ExtraField1,
        //                        ExtraField2 = detail.ExtraField2
        //                    }).ToList(),

        //                    OthersServices = service.OthersServices?.Select(other => new OthersServiceDTO
        //                    {
        //                        OthersId = other.OthersId,
        //                        ServiceId = other.ServiceId,
        //                        LableName = other.LableName,
        //                        Post = other.Post,
        //                        Note = other.Note,
        //                        ExtraField1 = other.ExtraField1,
        //                        ExtraField2 = other.ExtraField2
        //                    }).ToList(),
        //                    WebDevelopment = service.WebDevelopment?.Select(web => new WebDevelopmentDTO
        //                    {
        //                        WebDevelopmentId = web.WebDevelopmentId,
        //                        ServiceId = web.ServiceId,
        //                        DomainName = web.DomainName,
        //                        HostingDate = web.HostingDate,
        //                        HostingRenewalDate = web.HostingRenewalDate,
        //                        HostingLimit = web.HostingLimit,
        //                        HostingRenewalAmount = web.HostingRenewalAmount,
        //                        ServerFtpAssign = web.ServerFtpAssign,
        //                        ServerIp = web.ServerIp,
        //                        ServerUserId = web.ServerUserId,
        //                        ServerPassword = web.ServerPassword,
        //                        DesignTools = web.DesignTools,
        //                        MackupLink = web.MackupLink,
        //                        Languages = web.Languages,
        //                        IsActive = web.IsActive,
        //                        StartDate = web.StartDate,
        //                        Deadline = web.Deadline,
        //                        Remarks = web.Remarks,
        //                        Note = web.Note
        //                    }).ToList()

        //                }).ToList()
        //            }).ToList();




        //            return projectDTOs;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error retrieving projects: {ex.Message}");
        //        throw;
        //    }
        //}

        public async Task<List<ProjectsDTO?>> GetAllProjectAsync()
        {
            try
            {


                using (var context = dbContextFactory.CreateDbContext())
                {
                    var projects = await context.Projects
                        .Include(p => p.Client)
                        .Include(p => p.AssignedUsers)
                        .Include(p => p.Services)
                            .ThenInclude(s => s.SeoServiceDetails)
                        .Include(p => p.Services)
                            .ThenInclude(o => o.OthersServices)
                        .Include(p=>p.Services)
                            .ThenInclude(w=>w.WebDevelopment )
                        .ToListAsync();

                    var projectDTOs = projects.Select(project => new ProjectsDTO
                    {
                        ProjectId = project.ProjectId,
                        UserId = project.UserId,
                        ClientId = project.ClientId,
                        ProjectName = project.ProjectName,
                        BillingType = project.BillingType,
                        ProjectStartDate = project.ProjectStartDate,
                        ProjectType = project.ProjectType,
                        ProjectCost = project.ProjectCost,
                        CurrentIssue = project.CurrentIssue,
                        InternalRemark = project.InternalRemark,
                        CustomerNote = project.CustomerNote,
                        FbFollowers = project.FbFollowers,
                        IgFollowers = project.IgFollowers,
                        GmbRakning = project.GmbRakning,
                        IsActive = project.IsActive,

                        AssignedUsers = project.AssignedUsers?.Select(user => new AssignedUsersDTO
                        {
                            AssignedUsersId = user.AssignedUsersId,
                            ProjectId = user.ProjectId,
                            UserId = user.UserId,
                            UserName = user.UserName,
                            Role = user.Role
                        }).ToList(),

                        Client = project.Client != null ? new ClientsDTO
                        {
                            ClientId = project.Client.ClientId,
                            ClientName = project.Client.ClientName,
                            ClientEmail1 = project.Client.ClientEmail1,
                            ClientContact1 = project.Client.ClientContact1
                        } : null,

                        Services = project.Services.Select(service => new ServicesDTO
                        {
                            ServiceId = service.ServiceId,
                            ProjectId = service.ProjectId,
                            ProductsId = service.ProductsId, // ✅ Mapped correctly
                            ServiceName = service.ServiceName,
                            BillingType = service.BillingType,
                            Price = service.Price,
                            FinalPrice = service.FinalPrice,
                            StartDate = service.StartDate,
                            EndDate = service.EndDate,
                            TotalPost = service.TotalPost,
                            TotalReels = service.TotalReels,
                            AdsBudget = service.AdsBudget,
                            DeadLine = service.DeadLine,
                            ExtraField1 = service.ExtraField1,
                            ExtraField2 = service.ExtraField2,
                            ExtraField3 = service.ExtraField3,

                            SeoServiceDetails = service.SeoServiceDetails?.Select(detail => new SeoServiceDetailsDTO
                            {
                                SeoServiceDetailsId = detail.SeoServiceDetailsId,
                                ServiceId = detail.ServiceId,
                                KeyWord = detail.KeyWord,
                                Rank = detail.Rank,
                                Note = detail.Note,
                                ExtraField1 = detail.ExtraField1,
                                ExtraField2 = detail.ExtraField2
                            }).ToList(),

                            OthersServices = service.OthersServices?.Select(other => new OthersServiceDTO
                            {
                                OthersId = other.OthersId,
                                ServiceId = other.ServiceId,
                                LableName = other.LableName,
                                Post = other.Post,
                                Note = other.Note,
                                ExtraField1 = other.ExtraField1,
                                ExtraField2 = other.ExtraField2
                            }).ToList(),
                            WebDevelopment=service.WebDevelopment?.Select(web=> new WebDevelopmentDTO
                            {
                                WebDevelopmentId=web.WebDevelopmentId,
                                ServiceId = web.ServiceId,
                                DomainName = web.DomainName,
                                HostingDate = web.HostingDate,
                                HostingRenewalDate = web.HostingRenewalDate,
                                HostingLimit=web.HostingLimit,
                                HostingRenewalAmount = web.HostingRenewalAmount,
                                ServerFtpAssign = web.ServerFtpAssign,
                                ServerIp = web.ServerIp,
                                ServerUserId = web.ServerUserId,
                                ServerPassword = web.ServerPassword,
                                DesignTools = web.DesignTools,
                                MackupLink = web.MackupLink,
                                Languages = web.Languages,
                                IsActive = web.IsActive,
                                StartDate = web.StartDate,
                                Deadline = web.Deadline,
                                Remarks = web.Remarks,
                                Note = web.Note
                            }).ToList()
                        }).ToList()
                    }).ToList();

                    return projectDTOs;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving projects: {ex.Message}");
                throw;
            }
        }


        //public async Task<ProjectsDTO?> GetProjectByIdAsync(int projectId)
        //{
        //    using (var context = dbContextFactory.CreateDbContext())
        //    {
        //        var project = await context.Projects
        //            .Include(p => p.Client)
        //            .Include(p => p.WebDevelopment)
        //                .ThenInclude(w => w.DesignPhase)
        //            .Include(p => p.WebDevelopment)
        //                .ThenInclude(w => w.DevelopmentPhase)
        //            .Include(p => p.WebDevelopment)
        //                .ThenInclude(w => w.MaintenancePhase)
        //            .Include(p => p.MarketingPhase!)
        //                .ThenInclude(w => w.SocialMediaHandling)
        //            .Include(p => p.MarketingPhase!)
        //                .ThenInclude(w => w.Seo)
        //            .FirstOrDefaultAsync(p => p.ProjectId == projectId);

        //        if (project == null)
        //            return null;

        //        // Map to DTO
        //        var projectDto = new ProjectsDTO
        //        {
        //            ProjectId = project.ProjectId,
        //            ClientId = project.ClientId,
        //            ProjectName = project.ProjectName,
        //            ProjectStartDate = project.ProjectStartDate,
        //            ProjectCost = project.ProjectCost,
        //            ProjectType = project.ProjectType,
        //            ProjectCreatedAt = project.ProjectCreatedAt,
        //            IsActive=project.IsActive,
        //            Client = project.Client != null ? new ClientsDTO
        //            {
        //                ClientId = project.Client.ClientId,
        //                ClientName = project.Client.ClientName,
        //                ClientEmail1 = project.Client.ClientEmail1,
        //                ClientContact1 = project.Client.ClientContact1
        //                // Add more fields as needed
        //            } : null,
        //            WebDevelopment = project.WebDevelopment != null ? new WebDevelopmentDTO
        //            {
        //                WebDevelopmentId = project.WebDevelopment.WebDevelopmentId,
        //                ProjectIssueDate = project.WebDevelopment.ProjectIssueDate,
        //                ProjectDomainName = project.WebDevelopment.ProjectDomainName,
        //                ProjectHostingDate = (DateTime)project.WebDevelopment.ProjectHostingDate,
        //                ProjectHostingRenewalDate = (DateTime)project.WebDevelopment.ProjectHostingRenewalDate,
        //                ProjectHostingRenewalAmount = project.WebDevelopment.ProjectHostingRenewalAmount,
        //                ProjectServerFtpAssign = project.WebDevelopment.ProjectServerFtpAssign,
        //                ProjectServerIp = project.WebDevelopment.ProjectServerIp,
        //                ProjectServerUserId = project.WebDevelopment.ProjectServerUserId,
        //                ProjectServerPassword = project.WebDevelopment.ProjectServerPassword,
        //                ProjectHandledBy = project.WebDevelopment.ProjectHandledBy,
        //                ProjectStartDate = project.WebDevelopment.ProjectStartDate,
        //                ProjectDeadline = (DateTime)project.WebDevelopment.ProjectDeadline,
        //                ProjectExtendedDeadline = project.WebDevelopment.ProjectExtendedDeadline,
        //                ProjectIsActive = project.WebDevelopment.ProjectIsActive,
        //                ProjectRemarks = project.WebDevelopment.ProjectRemarks,
        //                ProjectIssueBy = project.WebDevelopment.ProjectIssueBy,
        //                DesignPhase = project.WebDevelopment.DesignPhase != null ? new DesignPhaseDTO
        //                {
        //                    DesignTaskId = project.WebDevelopment.DesignPhase.DesignTaskId,
        //                    WebDevelopmentId = project.WebDevelopment.DesignPhase.WebDevelopmentId,
        //                    Title = project.WebDevelopment.DesignPhase.Title,
        //                    Description = project.WebDevelopment.DesignPhase.Description,
        //                    Status = project.WebDevelopment.DesignPhase.Status,
        //                    DesignTool = project.WebDevelopment.DesignPhase.DesignTool,
        //                    MockupLink = project.WebDevelopment.DesignPhase.MockupLink,
        //                    FeedbackStatus = project.WebDevelopment.DesignPhase.FeedbackStatus,
        //                    StartTime = project.WebDevelopment.DesignPhase.StartTime,
        //                    EndTime = project.WebDevelopment.DesignPhase.EndTime,
        //                    DeadlineDate = project.WebDevelopment.DesignPhase.DeadlineDate
        //                } : null,
        //                DevelopmentPhase = project.WebDevelopment.DevelopmentPhase != null ? new DevelopmentPhaseDTO
        //                {
        //                    DevelopmentTaskId = project.WebDevelopment.DevelopmentPhase.DevelopmentTaskId,
        //                    WebDevelopmentId = project.WebDevelopment.DevelopmentPhase.WebDevelopmentId,
        //                    Title = project.WebDevelopment.DevelopmentPhase.Title,
        //                    Description = project.WebDevelopment.DevelopmentPhase.Description,
        //                    Status = project.WebDevelopment.DevelopmentPhase.Status,
        //                    ProgrammingLanguage = project.WebDevelopment.DevelopmentPhase.ProgrammingLanguage,
        //                    CodeRepoUrl = project.WebDevelopment.DevelopmentPhase.CodeRepoUrl,
        //                    TestStatus = project.WebDevelopment.DevelopmentPhase.TestStatus,
        //                    StartTime = project.WebDevelopment.DevelopmentPhase.StartTime,
        //                    EndTime = project.WebDevelopment.DevelopmentPhase.EndTime,
        //                    DeadlineDate = project.WebDevelopment.DevelopmentPhase.DeadlineDate
        //                } : null,
        //                MaintenancePhase = project.WebDevelopment.MaintenancePhase != null ? new MaintenancePhaseDTO
        //                {
        //                    MaintenanceId = project.WebDevelopment.MaintenancePhase.MaintenanceId,
        //                    WebDevelopmentId = project.WebDevelopment.MaintenancePhase.WebDevelopmentId,
        //                    Title = project.WebDevelopment.MaintenancePhase.Title,
        //                    Description = project.WebDevelopment.MaintenancePhase.Description,
        //                    Status = project.WebDevelopment.MaintenancePhase.Status,
        //                    IssueType = project.WebDevelopment.MaintenancePhase.IssueType,
        //                    Priority = project.WebDevelopment.MaintenancePhase.Priority,
        //                    SystemAffected = project.WebDevelopment.MaintenancePhase.SystemAffected,
        //                    StartTime = (DateTime)project.WebDevelopment.MaintenancePhase.StartTime
        //                } : null,
        //            } : null,
        //            MarketingPhase = project.MarketingPhase?.Select(m => new MarketingPhaseDTO
        //            {
        //                MarketingTaskId = m.MarketingTaskId,
        //                ProjectId = m.ProjectId,
        //                MarketingTypes = m.MarketingTypes,
        //                WebsiteUrl = m.WebsiteUrl,
        //                Title = m.Title,
        //                Description = m.Description,
        //                Status = m.Status,
        //                Budget = m.Budget,
        //                StartTime = m.StartTime,

        //                SocialMediaHandling = m.SocialMediaHandling != null ? new SocialMediaHandlingDTO
        //                {
        //                    SocialId = m.SocialMediaHandling.SocialId,
        //                    MarketingTaskId = m.SocialMediaHandling.MarketingTaskId,
        //                    StartTime = m.SocialMediaHandling.StartTime,
        //                    TotalPosts = (int)m.SocialMediaHandling.TotalPosts,
        //                    TotalFollowers = (int)m.SocialMediaHandling.TotalFollowers,
        //                    TotalLikes = (int)m.SocialMediaHandling.TotalLikes,
        //                    EngagementRate = (int)m.SocialMediaHandling.EngagementRate,
        //                    IssuedBy = m.SocialMediaHandling.IssuedBy,
        //                    ProgressStatus = m.SocialMediaHandling.ProgressStatus,
        //                    Notes = m.SocialMediaHandling.Notes
        //                } : null,

        //                Seo = m.Seo != null ? new SeoDTO
        //                {
        //                    SeoId = m.Seo.SeoId,
        //                    MarketingTaskId = m.Seo.MarketingTaskId,
        //                    StartTime = m.Seo.StartTime,
        //                    TotalPosts = m.Seo.TotalPosts,
        //                    Keyword = m.Seo.Keyword,
        //                    Ranking = (int)m.Seo.Ranking,
        //                    TotalFollowers = (int)m.Seo.TotalFollowers,
        //                    TotalLikes = (int)m.Seo.TotalLikes,
        //                    EngagementRate = (int)m.Seo.EngagementRate,
        //                    IssuedBy = m.Seo.IssuedBy,
        //                    ProgressStatus = m.Seo.ProgressStatus,
        //                    Notes = m.Seo.Notes
        //                } : null
        //            }).ToList()
        //        };

        //        return projectDto;
        //    }
        //}

        public async Task<ProjectsDTO?> GetProjectDTOById(int id)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var project = await context.Projects.FindAsync(id); // Pass the 'id' parameter
                return project == null ? null : Mapper.Map<ProjectsDTO>(project);
            }
        }

        public async Task UpdateProjectAsync(ProjectsDTO project)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                var existingProject = await context.Projects.FindAsync(project.ProjectId);
                if (existingProject != null)
                {
                    // Update basic fields
                    //existingProject.ClientId = project.ClientId;
                    //existingProject.UserId = project.UserId;
                    existingProject.IsActive = project.IsActive;
                    existingProject.ProjectName = project.ProjectName;
                    existingProject.CurrentIssue = project.CurrentIssue;
                    existingProject.InternalRemark = project.InternalRemark;
                    existingProject.CustomerNote = project.CustomerNote;
                    existingProject.FbFollowers = project.FbFollowers;
                    existingProject.IgFollowers = project.IgFollowers;
                    existingProject.GmbRakning = project.GmbRakning;
                    existingProject.ProjectStartDate = project.ProjectStartDate;
                    existingProject.ProjectCost = project.ProjectCost;
                    existingProject.BillingType = project.BillingType;
                    existingProject.ProjectType = project.ProjectType;
                    existingProject.ProjectCreatedAt = project.ProjectCreatedAt;

                    // Optional: Update child collections only if needed (custom logic may apply)
                    // This is a deep update, so be cautious and consider whether to:
                    // - Replace child objects entirely
                    // - Patch individual child elements
                    // For example:
                    // context.Entry(existingProject).Collection(p => p.MarketingPhase).CurrentValue = project.MarketingPhase;

                    // Save changes
                    await context.SaveChangesAsync();
                }
            }
        }

        //public async Task SaveOrUpdateProjectsAsync(ProjectsDTO projectDto)
        //{
        //    try
        //    {
        //        using var context = dbContextFactory.CreateDbContext();


        //            // Check if the project already exists
        //            var existingProject = await context.Projects
        //                .FirstOrDefaultAsync(p => p.ProjectId == projectDto.ProjectId);

        //            if (existingProject == null)
        //            {
        //                // Insert new project
        //                var newProject = new Projects
        //                {
        //                    ProjectId = (int)projectDto.ProjectId!,
        //                    UserId = (int)projectDto.UserId,
        //                    ClientId = (int)projectDto.ClientId,
        //                    ProjectName = projectDto.ProjectName,
        //                    BillingType = projectDto.BillingType,
        //                    ProjectStartDate = projectDto.ProjectStartDate,
        //                    ProjectType = projectDto.ProjectType,
        //                    ProjectCost = projectDto.ProjectCost,
        //                    CurrentIssue = projectDto.CurrentIssue,
        //                    InternalRemark = projectDto.InternalRemark,
        //                    CustomerNote = projectDto.CustomerNote,
        //                    FbFollowers = projectDto.FbFollowers,
        //                    IgFollowers = projectDto.IgFollowers,
        //                    GmbRakning = projectDto.GmbRakning,
        //                    IsActive = projectDto.IsActive
        //                };

        //                context.Projects.Add(newProject);
        //                await context.SaveChangesAsync();
        //            }
        //            else
        //            {
        //                // Update existing project
        //                existingProject.UserId = (int)projectDto.UserId;
        //                existingProject.ClientId = (int)projectDto.ClientId;
        //                existingProject.ProjectName = projectDto.ProjectName;
        //                existingProject.BillingType = projectDto.BillingType;
        //                existingProject.ProjectStartDate = projectDto.ProjectStartDate;
        //                existingProject.ProjectType = projectDto.ProjectType;
        //                existingProject.ProjectCost = projectDto.ProjectCost;
        //                existingProject.CurrentIssue = projectDto.CurrentIssue;
        //                existingProject.InternalRemark = projectDto.InternalRemark;
        //                existingProject.CustomerNote = projectDto.CustomerNote;
        //                existingProject.FbFollowers = projectDto.FbFollowers;
        //                existingProject.IgFollowers = projectDto.IgFollowers;
        //                existingProject.GmbRakning = projectDto.GmbRakning;
        //                existingProject.IsActive = projectDto.IsActive;

        //                context.Projects.Update(existingProject);
        //                await context.SaveChangesAsync();
        //            }

        //            // Handle AssignedUsers Upsert
        //            foreach (var userDto in projectDto.AssignedUsers ?? [])
        //            {
        //                try
        //                {
        //                    var existingUser = await context.AssignedUsers
        //                        .FirstOrDefaultAsync(x => x.AssignedUsersId == userDto.AssignedUsersId);

        //                    if (existingUser == null)
        //                    {
        //                        context.AssignedUsers.Add(new AssignedUsers
        //                        {
        //                            AssignedUsersId = (int)userDto.AssignedUsersId,
        //                            ProjectId = (int)userDto.ProjectId,
        //                            UserId = (int)userDto.UserId,
        //                            UserName = userDto.UserName,
        //                            Role = userDto.Role
        //                        });
        //                    }
        //                    else
        //                    {
        //                        existingUser.UserId = (int)userDto.UserId;
        //                        existingUser.UserName = userDto.UserName;
        //                        existingUser.Role = userDto.Role;

        //                        context.AssignedUsers.Update(existingUser);
        //                    }

        //                    await context.SaveChangesAsync();
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine($"Error saving AssignedUser: {ex.Message}");
        //                }
        //            }

        //            // Handle Services Upsert
        //            foreach (var serviceDto in projectDto.Services ?? [])
        //            {
        //                try
        //                {
        //                    var existingService = await context.Services
        //                        .FirstOrDefaultAsync(s => s.ServiceId == serviceDto.ServiceId);

        //                    if (existingService == null)
        //                    {
        //                        var newService = new Services
        //                        {
        //                            ServiceId = (int)serviceDto.ServiceId,
        //                            ProjectId = serviceDto.ProjectId,
        //                            ProductsId = serviceDto.ProductsId,
        //                            ServiceName = serviceDto.ServiceName,
        //                            BillingType = serviceDto.BillingType,
        //                            Price = serviceDto.Price,
        //                            FinalPrice = serviceDto.FinalPrice,
        //                            StartDate = serviceDto.StartDate,
        //                            EndDate = serviceDto.EndDate,
        //                            TotalPost = serviceDto.TotalPost,
        //                            TotalReels = serviceDto.TotalReels,
        //                            AdsBudget = serviceDto.AdsBudget,
        //                            DeadLine = serviceDto.DeadLine,
        //                            ExtraField1 = serviceDto.ExtraField1,
        //                            ExtraField2 = serviceDto.ExtraField2,
        //                            ExtraField3 = serviceDto.ExtraField3
        //                        };
        //                        context.Services.Add(newService);
        //                        await context.SaveChangesAsync();
        //                        existingService = newService; // For nested children
        //                    }
        //                    else
        //                    {
        //                        existingService.ProductsId = serviceDto.ProductsId;
        //                        existingService.ServiceName = serviceDto.ServiceName;
        //                        existingService.BillingType = serviceDto.BillingType;
        //                        existingService.Price = serviceDto.Price;
        //                        existingService.FinalPrice = serviceDto.FinalPrice;
        //                        existingService.StartDate = serviceDto.StartDate;
        //                        existingService.EndDate = serviceDto.EndDate;
        //                        existingService.TotalPost = serviceDto.TotalPost;
        //                        existingService.TotalReels = serviceDto.TotalReels;
        //                        existingService.AdsBudget = serviceDto.AdsBudget;
        //                        existingService.DeadLine = serviceDto.DeadLine;
        //                        existingService.ExtraField1 = serviceDto.ExtraField1;
        //                        existingService.ExtraField2 = serviceDto.ExtraField2;
        //                        existingService.ExtraField3 = serviceDto.ExtraField3;

        //                        context.Services.Update(existingService);
        //                        await context.SaveChangesAsync();
        //                    }

        //                    // SeoServiceDetails Upsert
        //                    foreach (var seoDto in serviceDto.SeoServiceDetails ?? [])
        //                    {
        //                        try
        //                        {
        //                            var existingSeo = await context.SeoServiceDetails
        //                                .FirstOrDefaultAsync(x => x.SeoServiceDetailsId == seoDto.SeoServiceDetailsId);

        //                            if (existingSeo == null)
        //                            {
        //                                context.SeoServiceDetails.Add(new SeoServiceDetails
        //                                {
        //                                    SeoServiceDetailsId = (int)seoDto.SeoServiceDetailsId,
        //                                    ServiceId = existingService.ServiceId,
        //                                    KeyWord = seoDto.KeyWord,
        //                                    Rank = seoDto.Rank,
        //                                    Note = seoDto.Note,
        //                                    ExtraField1 = seoDto.ExtraField1,
        //                                    ExtraField2 = seoDto.ExtraField2
        //                                });
        //                            }
        //                            else
        //                            {
        //                                existingSeo.KeyWord = seoDto.KeyWord;
        //                                existingSeo.Rank = seoDto.Rank;
        //                                existingSeo.Note = seoDto.Note;
        //                                existingSeo.ExtraField1 = seoDto.ExtraField1;
        //                                existingSeo.ExtraField2 = seoDto.ExtraField2;

        //                                context.SeoServiceDetails.Update(existingSeo);
        //                            }

        //                            await context.SaveChangesAsync();
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            Console.WriteLine($"Error in SeoServiceDetails: {ex.Message}");
        //                        }
        //                    }

        //                    // OthersServices Upsert
        //                    foreach (var otherDto in serviceDto.OthersServices ?? [])
        //                    {
        //                        try
        //                        {
        //                            var existingOther = await context.OthersService
        //                                .FirstOrDefaultAsync(x => x.OthersId == otherDto.OthersId);

        //                            if (existingOther == null)
        //                            {
        //                                context.OthersService.Add(new OthersService
        //                                {
        //                                    OthersId = (int)otherDto.OthersId,
        //                                    ServiceId = existingService.ServiceId,
        //                                    LableName = otherDto.LableName,
        //                                    Post = otherDto.Post,
        //                                    Note = otherDto.Note,
        //                                    ExtraField1 = otherDto.ExtraField1,
        //                                    ExtraField2 = otherDto.ExtraField2
        //                                });
        //                            }
        //                            else
        //                            {
        //                                existingOther.LableName = otherDto.LableName;
        //                                existingOther.Post = otherDto.Post;
        //                                existingOther.Note = otherDto.Note;
        //                                existingOther.ExtraField1 = otherDto.ExtraField1;
        //                                existingOther.ExtraField2 = otherDto.ExtraField2;

        //                                context.OthersService.Update(existingOther);
        //                            }

        //                            await context.SaveChangesAsync();
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            Console.WriteLine($"Error in OthersServices: {ex.Message}");
        //                        }
        //                    }
        //                // Web Development Services
        //                foreach (var webDto in serviceDto.WebDevelopment ?? [])
        //                {
        //                    try
        //                    {
        //                        var existingWeb = await context.WebDevelopment
        //                            .FirstOrDefaultAsync(x => x.WebDevelopmentId == webDto.WebDevelopmentId);

        //                        if (existingWeb == null)
        //                        {
        //                            var newWeb = new WebDevelopment
        //                            {
        //                                ServiceId = existingService.ServiceId,
        //                                DomainName = webDto.DomainName,
        //                                HostingDate = webDto.HostingDate,
        //                                HostingRenewalDate = webDto.HostingRenewalDate,
        //                                HostingLimit = webDto.HostingLimit,
        //                                HostingRenewalAmount = webDto.HostingRenewalAmount,
        //                                ServerFtpAssign = webDto.ServerFtpAssign,
        //                                ServerIp = webDto.ServerIp,
        //                                ServerUserId = webDto.ServerUserId,
        //                                ServerPassword = webDto.ServerPassword,
        //                                DesignTools = webDto.DesignTools,
        //                                MackupLink = webDto.MackupLink,
        //                                Languages = webDto.Languages,
        //                                IsActive = webDto.IsActive,
        //                                StartDate = webDto.StartDate,
        //                                Deadline = webDto.Deadline,
        //                                Remarks = webDto.Remarks,
        //                                Note = webDto.Note
        //                            };
        //                            context.WebDevelopment.Add(newWeb);
        //                        }
        //                        else
        //                        {
        //                            existingWeb.DomainName = webDto.DomainName;
        //                            existingWeb.HostingDate = webDto.HostingDate;
        //                            existingWeb.HostingRenewalDate = webDto.HostingRenewalDate;
        //                            existingWeb.HostingRenewalAmount = webDto.HostingRenewalAmount;
        //                            existingWeb.HostingLimit = webDto.HostingLimit;
        //                            existingWeb.ServerFtpAssign = webDto.ServerFtpAssign;
        //                            existingWeb.ServerIp = webDto.ServerIp;
        //                            existingWeb.ServerUserId = webDto.ServerUserId;
        //                            existingWeb.ServerPassword = webDto.ServerPassword;
        //                            existingWeb.DesignTools = webDto.DesignTools;
        //                            existingWeb.MackupLink = webDto.MackupLink;
        //                            existingWeb.Languages = webDto.Languages;
        //                            existingWeb.IsActive = webDto.IsActive;
        //                            existingWeb.StartDate = webDto.StartDate;
        //                            existingWeb.Deadline = webDto.Deadline;
        //                            existingWeb.Remarks = webDto.Remarks;
        //                            existingWeb.Note = webDto.Note;
        //                            context.WebDevelopment.Update(existingWeb);
        //                        }

        //                        await context.SaveChangesAsync();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine($"Error in WebDevelopment service upsert: {ex.Message}");
        //                    }
        //                }
        //            }

        //            catch (Exception ex)
        //                {
        //                    Console.WriteLine($"Error saving Service: {ex.Message}");
        //                }
        //            }

        //    }

        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Critical failure: {ex.Message}");
        //        throw;
        //    }
        //}
        public async Task SaveOrUpdateProjectsAsync(ProjectsDTO projectDto)
        {
            try
            {
                using var context = dbContextFactory.CreateDbContext();

                Projects? existingProject = null;
                int projectId;

                if (projectDto.ProjectId > 0)
                {
                    existingProject = await context.Projects.FirstOrDefaultAsync(p => p.ProjectId == projectDto.ProjectId);
                }

                if (existingProject == null)
                {
                    var newProject = new Projects
                    {
                        UserId = (int)projectDto.UserId,
                        ClientId = (int)projectDto.ClientId,
                        ProjectName = projectDto.ProjectName,
                        BillingType = projectDto.BillingType,
                        ProjectStartDate = projectDto.ProjectStartDate,
                        ProjectType = projectDto.ProjectType,
                        ProjectCost = projectDto.ProjectCost,
                        CurrentIssue = projectDto.CurrentIssue,
                        InternalRemark = projectDto.InternalRemark,
                        CustomerNote = projectDto.CustomerNote,
                        FbFollowers = projectDto.FbFollowers,
                        IgFollowers = projectDto.IgFollowers,
                        GmbRakning = projectDto.GmbRakning,
                        IsActive = projectDto.IsActive
                    };

                    context.Projects.Add(newProject);
                    await context.SaveChangesAsync();
                    projectId = newProject.ProjectId; // ✅ get generated ID
                }
                else
                {
                    existingProject.UserId = (int)projectDto.UserId;
                    existingProject.ClientId = (int)projectDto.ClientId;
                    existingProject.ProjectName = projectDto.ProjectName;
                    existingProject.BillingType = projectDto.BillingType;
                    existingProject.ProjectStartDate = projectDto.ProjectStartDate;
                    existingProject.ProjectType = projectDto.ProjectType;
                    existingProject.ProjectCost = projectDto.ProjectCost;
                    existingProject.CurrentIssue = projectDto.CurrentIssue;
                    existingProject.InternalRemark = projectDto.InternalRemark;
                    existingProject.CustomerNote = projectDto.CustomerNote;
                    existingProject.FbFollowers = projectDto.FbFollowers;
                    existingProject.IgFollowers = projectDto.IgFollowers;
                    existingProject.GmbRakning = projectDto.GmbRakning;
                    existingProject.IsActive = projectDto.IsActive;

                    context.Projects.Update(existingProject);
                    await context.SaveChangesAsync();
                    projectId = existingProject.ProjectId;
                }

                // ✅ AssignedUsers Upsert
                foreach (var userDto in projectDto.AssignedUsers ?? [])
                {
                    try
                    {
                        var existingUser = await context.AssignedUsers
                            .FirstOrDefaultAsync(x => x.AssignedUsersId == userDto.AssignedUsersId);

                        if (existingUser == null)
                        {
                            context.AssignedUsers.Add(new AssignedUsers
                            {
                                ProjectId = projectId,
                                UserId = (int)userDto.UserId,
                                UserName = userDto.UserName,
                                Role = userDto.Role
                            });
                        }
                        else
                        {
                            existingUser.UserId = (int)userDto.UserId;
                            existingUser.UserName = userDto.UserName;
                            existingUser.Role = userDto.Role;

                            context.AssignedUsers.Update(existingUser);
                        }

                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving AssignedUser: {ex.Message}");
                    }
                }

                // ✅ Services + Nested Children
                foreach (var serviceDto in projectDto.Services ?? [])
                {
                    try
                    {
                        var existingService = await context.Services
                            .FirstOrDefaultAsync(s => s.ServiceId == serviceDto.ServiceId);

                        if (existingService == null)
                        {
                            var newService = new Services
                            {
                                ProjectId = projectId,
                                ProductsId = serviceDto.ProductsId,
                                ServiceName = serviceDto.ServiceName,
                                BillingType = serviceDto.BillingType,
                                Price = serviceDto.Price,
                                FinalPrice = serviceDto.FinalPrice,
                                StartDate = serviceDto.StartDate,
                                EndDate = serviceDto.EndDate,
                                TotalPost = serviceDto.TotalPost,
                                TotalReels = serviceDto.TotalReels,
                                AdsBudget = serviceDto.AdsBudget,
                                DeadLine = serviceDto.DeadLine,
                                ExtraField1 = serviceDto.ExtraField1,
                                ExtraField2 = serviceDto.ExtraField2,
                                ExtraField3 = serviceDto.ExtraField3
                            };
                            context.Services.Add(newService);
                            await context.SaveChangesAsync();
                            existingService = newService;
                        }
                        else
                        {
                            existingService.ProductsId = serviceDto.ProductsId;
                            existingService.ServiceName = serviceDto.ServiceName;
                            existingService.BillingType = serviceDto.BillingType;
                            existingService.Price = serviceDto.Price;
                            existingService.FinalPrice = serviceDto.FinalPrice;
                            existingService.StartDate = serviceDto.StartDate;
                            existingService.EndDate = serviceDto.EndDate;
                            existingService.TotalPost = serviceDto.TotalPost;
                            existingService.TotalReels = serviceDto.TotalReels;
                            existingService.AdsBudget = serviceDto.AdsBudget;
                            existingService.DeadLine = serviceDto.DeadLine;
                            existingService.ExtraField1 = serviceDto.ExtraField1;
                            existingService.ExtraField2 = serviceDto.ExtraField2;
                            existingService.ExtraField3 = serviceDto.ExtraField3;

                            context.Services.Update(existingService);
                            await context.SaveChangesAsync();
                        }

                        // SeoServiceDetails
                        foreach (var seoDto in serviceDto.SeoServiceDetails ?? [])
                        {
                            try
                            {
                                var existingSeo = await context.SeoServiceDetails
                                    .FirstOrDefaultAsync(x => x.SeoServiceDetailsId == seoDto.SeoServiceDetailsId);

                                if (existingSeo == null)
                                {
                                    context.SeoServiceDetails.Add(new SeoServiceDetails
                                    {
                                        ServiceId = existingService.ServiceId,
                                        KeyWord = seoDto.KeyWord,
                                        Rank = seoDto.Rank,
                                        Note = seoDto.Note,
                                        ExtraField1 = seoDto.ExtraField1,
                                        ExtraField2 = seoDto.ExtraField2
                                    });
                                }
                                else
                                {
                                    existingSeo.KeyWord = seoDto.KeyWord;
                                    existingSeo.Rank = seoDto.Rank;
                                    existingSeo.Note = seoDto.Note;
                                    existingSeo.ExtraField1 = seoDto.ExtraField1;
                                    existingSeo.ExtraField2 = seoDto.ExtraField2;

                                    context.SeoServiceDetails.Update(existingSeo);
                                }

                                await context.SaveChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error in SeoServiceDetails: {ex.Message}");
                            }
                        }

                        // OthersService
                        foreach (var otherDto in serviceDto.OthersServices ?? [])
                        {
                            try
                            {
                                var existingOther = await context.OthersService
                                    .FirstOrDefaultAsync(x => x.OthersId == otherDto.OthersId);

                                if (existingOther == null)
                                {
                                    context.OthersService.Add(new OthersService
                                    {
                                        ServiceId = existingService.ServiceId,
                                        LableName = otherDto.LableName,
                                        Post = otherDto.Post,
                                        Note = otherDto.Note,
                                        ExtraField1 = otherDto.ExtraField1,
                                        ExtraField2 = otherDto.ExtraField2
                                    });
                                }
                                else
                                {
                                    existingOther.LableName = otherDto.LableName;
                                    existingOther.Post = otherDto.Post;
                                    existingOther.Note = otherDto.Note;
                                    existingOther.ExtraField1 = otherDto.ExtraField1;
                                    existingOther.ExtraField2 = otherDto.ExtraField2;

                                    context.OthersService.Update(existingOther);
                                }

                                await context.SaveChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error in OthersService: {ex.Message}");
                            }
                        }

                        // WebDevelopment
                        foreach (var webDto in serviceDto.WebDevelopment ?? [])
                        {
                            try
                            {
                                var existingWeb = await context.WebDevelopment
                                    .FirstOrDefaultAsync(x => x.WebDevelopmentId == webDto.WebDevelopmentId);

                                if (existingWeb == null)
                                {
                                    context.WebDevelopment.Add(new WebDevelopment
                                    {
                                        ServiceId = existingService.ServiceId,
                                        DomainName = webDto.DomainName,
                                        HostingDate = webDto.HostingDate,
                                        HostingRenewalDate = webDto.HostingRenewalDate,
                                        HostingLimit = webDto.HostingLimit,
                                        HostingRenewalAmount = webDto.HostingRenewalAmount,
                                        ServerFtpAssign = webDto.ServerFtpAssign,
                                        ServerIp = webDto.ServerIp,
                                        ServerUserId = webDto.ServerUserId,
                                        ServerPassword = webDto.ServerPassword,
                                        DesignTools = webDto.DesignTools,
                                        MackupLink = webDto.MackupLink,
                                        Languages = webDto.Languages,
                                        IsActive = webDto.IsActive,
                                        StartDate = webDto.StartDate,
                                        Deadline = webDto.Deadline,
                                        Remarks = webDto.Remarks,
                                        Note = webDto.Note
                                    });
                                }
                                else
                                {
                                    existingWeb.DomainName = webDto.DomainName;
                                    existingWeb.HostingDate = webDto.HostingDate;
                                    existingWeb.HostingRenewalDate = webDto.HostingRenewalDate;
                                    existingWeb.HostingLimit = webDto.HostingLimit;
                                    existingWeb.HostingRenewalAmount = webDto.HostingRenewalAmount;
                                    existingWeb.ServerFtpAssign = webDto.ServerFtpAssign;
                                    existingWeb.ServerIp = webDto.ServerIp;
                                    existingWeb.ServerUserId = webDto.ServerUserId;
                                    existingWeb.ServerPassword = webDto.ServerPassword;
                                    existingWeb.DesignTools = webDto.DesignTools;
                                    existingWeb.MackupLink = webDto.MackupLink;
                                    existingWeb.Languages = webDto.Languages;
                                    existingWeb.IsActive = webDto.IsActive;
                                    existingWeb.StartDate = webDto.StartDate;
                                    existingWeb.Deadline = webDto.Deadline;
                                    existingWeb.Remarks = webDto.Remarks;
                                    existingWeb.Note = webDto.Note;

                                    context.WebDevelopment.Update(existingWeb);
                                }

                                await context.SaveChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error in WebDevelopment: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving Service: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical failure in SaveOrUpdateProjectsAsync: {ex.Message}");
                throw;
            }
        }
        public Task<ProjectsDTO?> GetProjectByIdAsync(int projectId)
        {
            throw new NotImplementedException();
        }
    }


}
