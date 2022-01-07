using Project_Management.DAL.Database;

namespace Project_Management.DAL.Entities
{
    public class DbSeeder
    {
        public static void Seed(ProjectManagementContext database)
        {
            if (database.Database.EnsureCreated())
            {
                database.Users.Add(new User()
                {
                    Username = "admin",
                    Password = "adminpass",
                    Role = Role.ADMIN,
                    FirstName = "TheBoss",
                    LastName = "OfThemAll"
                });
                database.SaveChanges();
            }
        }
    }
}
