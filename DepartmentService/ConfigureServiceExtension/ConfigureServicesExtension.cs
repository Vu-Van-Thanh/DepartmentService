using DepartmentService.API.AppDbContext;
using DepartmentService.API.Repositories;
using DepartmentService.API.Services;
using DepartmentService.Kafka;
using DepartmentService.Kafka.Consumers.Manual;
using DepartmentService.Kafka.Producer;
using DepartmentService.Repositories;
using DepartmentService.Services;
using EmployeeService.Infrastructure.Kafka.Consumers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace DepartmentService.ConfigureServiceExtension
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Add Repositories
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<IOwnFacilitiesRepository, OwnFacilitiesRepository>();
            services.AddScoped<IMediaRepository, MediaRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<IJobPositionRepository, JobPositionRepository>();


            // Add Services
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IDepartmentService, DepartmentServices>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IOwnFacilitiesService, OwnFacilitiesService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectTaskService, ProjectTaskService>();
            services.AddScoped<IJobPositionService, JobPositionService>();
            services.Configure<KafkaSettings>(configuration.GetSection("Kafka"));
            services.AddScoped<IEventProducer, DepartmentProducer>();
            services.AddScoped<IEventConsumer, ManualDepartmentConsumer>();
           // services.AddHostedService<DepartmentConsumer>();

            // Add Controllers
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            // Add Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Department Service API", Version = "v1" });
            });
        }
    }
}
