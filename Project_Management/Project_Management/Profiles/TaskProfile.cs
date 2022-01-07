using AutoMapper;
using Project_Management.DAL.Entities;
using Project_Management.DTO_Models.Requests.Tasks;
using Project_Management.DTO_Models.Responses.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Project_Management.WEB.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            this.CreateMap<Task, TaskRequest>().ReverseMap();
            this.CreateMap<Task, TaskResponse>().ReverseMap();
        }
    }
}
