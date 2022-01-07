using AutoMapper;
using Project_Management.DAL.Entities;
using Project_Management.DTO_Models.Responses.Worklogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.WEB.Profiles
{
    public class WorklogProfile : Profile
    {
        public WorklogProfile()
        {
            this.CreateMap<WorkLog, WorklogResponse>().ReverseMap();
        }
    }
}
