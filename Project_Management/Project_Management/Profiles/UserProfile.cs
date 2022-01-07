using AutoMapper;
using Project_Management.DAL.Entities;
using Project_Management.DTO_Models.Requests.Users;
using Project_Management.DTO_Models.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.WEB.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserLogin>().ReverseMap();
            this.CreateMap<User, UserReg>().ReverseMap();
            this.CreateMap<User, UserResponse>().ReverseMap();
        }
    }
}
