using System.Text.Json;
using DepartmentService.API.Entities;
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

        public DbSet<OwnFacilities> OwnFacilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            //Seed data
            var departments = LoadSeedData<Department>("SeedData/Departments.json");
            builder.Entity<Department>().HasData(departments);

            var facilities = LoadSeedData<Facility>("SeedData/Facilities.json");
            builder.Entity<Facility>().HasData(facilities);

            var jobs = LoadSeedData<JobPosition>("SeedData/JobPositions.json");
            builder.Entity<JobPosition>().HasData(jobs);

            var OwnFacilities = LoadSeedData<OwnFacilities>("SeedData/OwnFacilities.json");
            builder.Entity<OwnFacilities>().HasData(OwnFacilities);
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
