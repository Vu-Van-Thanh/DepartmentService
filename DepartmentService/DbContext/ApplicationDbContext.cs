using System.Text.Json;
using DepartmentService.API.Entities;
using DepartmentService.Entities;
using Microsoft.EntityFrameworkCore;


namespace DepartmentService.API.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<AppliedCv> AppliedCvs { get; set; }

        public DbSet<OwnFacilities> OwnFacilities { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure composite key for OwnFacilities
            builder.Entity<OwnFacilities>()
                .HasKey(of => new { of.OwnerId, of.FacilityId });

            // Configure relationships and constraints
            builder.Entity<Department>()
                .HasMany(d => d.JobPositions)
                .WithOne(j => j.Department)
                .HasForeignKey(j => j.DepartmentId);

            builder.Entity<Article>()
                .HasMany(a => a.medias)
                .WithOne(m => m.Article)
                .HasForeignKey(m => m.ArticleID);

            builder.Entity<Project>()
                .HasOne(p => p.Department)
                .WithMany(d => d.Projects)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProjectTask>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<JobPosition>()
                .HasOne(j => j.Department)
                .WithMany(d => d.JobPositions)
                .HasForeignKey(j => j.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

         
            builder.Entity<Article>()
                .HasOne(a => a.Department)
                .WithMany(d => d.Articles)
                .HasForeignKey(a => a.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            //Seed data
            var departments = LoadSeedData<Department>("SeedData/Departments.json");
            builder.Entity<Department>().HasData(departments);

            var facilities = LoadSeedData<Facility>("SeedData/Facilities.json");
            builder.Entity<Facility>().HasData(facilities);

            var jobs = LoadSeedData<JobPosition>("SeedData/JobPositions.json");
            builder.Entity<JobPosition>().HasData(jobs);

            var OwnFacilities = LoadSeedData<OwnFacilities>("SeedData/OwnFacilities.json");
            builder.Entity<OwnFacilities>().HasData(OwnFacilities);

            var articles = LoadSeedData<Article>("SeedData/Articles.json");
            builder.Entity<Article>().HasData(articles);

            var medias = LoadSeedData<Media>("SeedData/Medias.json");
            builder.Entity<Media>().HasData(medias);

            var projects = LoadSeedData<Project>("SeedData/Projects.json");
            builder.Entity<Project>().HasData(projects);

            var tasks = LoadSeedData<ProjectTask>("SeedData/Tasks.json");
            builder.Entity<ProjectTask>().HasData(tasks);
        }

        private static List<T> LoadSeedData<T>(string filePath)
        {
            string projectRoot = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(projectRoot, filePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Không tìm thấy file seed data: {fullPath}");

            var jsonData = File.ReadAllText(fullPath);
            var items = JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();

            return items;
        }
    }
}
