using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Project_Management.BLL.Services;
using Project_Management.DAL.Database;
using Project_Management.DAL.Entities;
using Project_Management.DAL.Repositories;
using Project_Management.DAL.Repositories.IRepositories;
using Project_Management.WEB.Filters;

namespace Project_Management.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProjectManagementContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<UserService>();

            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<TeamService>();

            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<ProjectService>();

            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<TaskService>();

            services.AddTransient<IWorklogRepository, WorklogRepository>();
            services.AddTransient<WorklogService>();

            services.AddAutoMapper(typeof(Startup));
           

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project_Management", Version = "v1" });
                c.OperationFilter<UserHeaderFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ProjectManagementContext>();
                DbSeeder.Seed(context);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project_Management v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
