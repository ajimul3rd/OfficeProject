using Microsoft.EntityFrameworkCore;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Web.Pages;
namespace OfficeProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserDesignation> UserDesignation { get; set; }
        public DbSet<Clients> Client { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<WebDevelopment> WebDevelopment { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<AssignedUsers> AssignedUsers { get; set; }
        public DbSet<OthersService> OthersService { get; set; }
        public DbSet<PaymentSchedule> PaymentSchedule { get; set; }
        public DbSet<SeoServiceDetails> SeoServiceDetails { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<WorkTaskDetails> WorkRecords { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<ClientSources> clientSources { get; set; }
        public DbSet<UserWorkingActivity> UserWorkingActivity { get; set; }
        public DbSet<UserActivityMaster> UserWorkingActivityList { get; set; }
        public DbSet<OthersTaskDetails> OthersTaskDetails { get; set; }
        public DbSet<SeoTaskDetails> SeoTaskDetails { get; set; }
        public DbSet<WebDeveTaskDetails> WebDeveTaskDetails { get; set; }
        public DbSet<UserTaskMaster> UserTask { get; set; }
        public DbSet<SpacificUserTask> SpacificUserTask { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




            modelBuilder.Entity<AssignedUsers>()
            .HasOne(a => a.Users)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Projects>()
    .HasOne(p => p.Users)
    .WithMany()
    .HasForeignKey(p => p.UserId)
    .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Projects>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Services>()
                  .HasOne(s => s.Products)
                  .WithMany()
                  .HasForeignKey(s => s.ProductsId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Services>()
                .HasOne(s => s.Projects)
                .WithMany()
                .HasForeignKey(s => s.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Projects>()
                .HasMany(p => p.Services)
                .WithOne(s => s.Projects)
                .HasForeignKey(s => s.ProjectId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Services>()
                .HasOne(s => s.Products)
                .WithMany()
                .HasForeignKey(s => s.ProductsId)
                .OnDelete(DeleteBehavior.ClientNoAction);
            modelBuilder.Entity<Services>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.FinalPrice).HasPrecision(18, 2);
                entity.Property(e => e.AdsBudget).HasPrecision(18, 2);
            });


        }
    }
}