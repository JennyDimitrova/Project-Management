using Microsoft.EntityFrameworkCore;
using Project_Management.DAL.Entities;

namespace Project_Management.DAL.Database
{
    public class ProjectManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<WorkLog> WorkLogs { get; set; }
        public ProjectManagementContext() : base() { }
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options) { }

    }
}
