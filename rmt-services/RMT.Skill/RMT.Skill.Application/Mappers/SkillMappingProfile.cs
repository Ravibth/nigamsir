using AutoMapper;
using RMT.Skill.Application.Handlers.CommandHandler;
using RMT.Skill.Application.Handlers.QueryHandler;
using RMT.Skill.Domain.DTOs;
using RMT.Skill.Domain.DTOs.Responses;
using RMT.Skill.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Mappers
{
    public class SkillMappingProfile : Profile
    {
        public SkillMappingProfile()
        {
            CreateMap<SkillSubmitCommand, SkillSubmitDTO>().ReverseMap();
            CreateMap<SkillMappingDTO, SkillMapping>()
                .ForMember(dest => dest.Competency , opt => opt.MapFrom(src => src.Competency.Competency))
                .ForMember(dest => dest.CompetencyId, opt => opt.MapFrom(src => src.Competency.CompetencyId))
                .ReverseMap();
            CreateMap<SkillUpdateCommand, SkillSubmitDTO>().ReverseMap();
            CreateMap<SkillStatusUpdateCommand, UpdateSkillStatusDTO>().ReverseMap();
            CreateMap<UserSkills, AddUpdateNewUserSkill>().ReverseMap();
            CreateMap<MandetorySkillRequestDTO, GetMenatorySkillQuery>().ReverseMap();
            CreateMap<UserSkills, UserSkillsResponseWithSkillDTO>().ReverseMap();
        }

    }
}
