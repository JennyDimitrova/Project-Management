using AutoMapper;
using Project_Management.DAL.Entities;
using Project_Management.DTO_Models.Requests.Teams;
using Project_Management.DTO_Models.Responses.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.WEB.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            this.CreateMap<Team, TeamRequest>().ReverseMap();
            this.CreateMap<Team, TeamResponse>().ReverseMap();
        }

    }
}
