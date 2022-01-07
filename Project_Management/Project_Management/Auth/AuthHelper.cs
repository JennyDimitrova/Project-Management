using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Project_Management.BLL.Services;
using Project_Management.DAL.Entities;

namespace Project_Management.WEB.Auth
{
    public static class AuthHelper
    {
        public static User GetCurrentUser(this UserService userService, HttpRequest request)
        {
            StringValues userHeaders;
            StringValues passHeaders;
            request.Headers.TryGetValue("Username", out userHeaders);
            request.Headers.TryGetValue("Password", out passHeaders);

            if (userHeaders.Count != 0 && passHeaders.Count != 0)
            {
                string username = userHeaders.First();
                string password = passHeaders.First();
                return userService.GetUserByCredentials(username, password);
            }
            return null;
        }
    }
}
