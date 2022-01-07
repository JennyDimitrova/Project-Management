using AutoMapper;
using Project_Management.DAL.Entities;
using Project_Management.DTO_Models.Requests.Projects;
using Project_Management.DTO_Models.Responses.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.WEB.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            this.CreateMap<Project, ProjectRequest>().ReverseMap();
            this.CreateMap<Project, ProjectResponse>().ReverseMap();
        }
    }
}
