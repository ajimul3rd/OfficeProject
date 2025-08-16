using AutoMapper;
using OfficeProject.Authentication;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;

namespace OfficeProject.Models.Mapings;

public class MapingProfile:Profile
    {

    public MapingProfile()
    {
        CreateMap<ClientsDTO, Clients>().ReverseMap();
        //CreateMap<DesignPhaseDTO, DesignPhase>().ReverseMap();
        //CreateMap<DevelopmentPhaseDTO, DevelopmentPhase>().ReverseMap();
        //CreateMap<MaintenancePhaseDTO, MaintenancePhase>().ReverseMap();
        //CreateMap<DevelopmentPhaseDTO, DevelopmentPhase>().ReverseMap();
        CreateMap<ProjectsDTO, Projects>().ReverseMap();
        CreateMap<WebDevelopmentDTO, WebDevelopment>().ReverseMap();
        CreateMap<ProjectsDTO, Projects>().ReverseMap();
        //CreateMap<MarketingPhaseDTO, MarketingPhase>().ReverseMap();
        //CreateMap<SeoDTO, Seo>().ReverseMap();
        CreateMap<UserActivityMasterDto, UserActivityMaster>().ReverseMap();
        CreateMap<RegisterModel,Users>().ReverseMap();
        CreateMap<UserDTO, RegisterModel>().ReverseMap();
        CreateMap<UserWithClientsDto, Users>().ReverseMap(); 
        CreateMap<UserDTO, Users>().ReverseMap();
        CreateMap<UserDesignation, UserDesignationDto>().ReverseMap();
        CreateMap<Users, UserDTO>()
            .ForMember(dest => dest.UserDesignation, opt => opt.MapFrom(src => src.UserDesignation))
            .ReverseMap();

        CreateMap<ServicesDTO, Services>().ReverseMap();
        CreateMap<ProductsDTO, Products>().ReverseMap();
        CreateMap<WorkTaskDetailsDto, WorkTaskDetails>().ReverseMap();
        CreateMap<OthersServiceDTO, OthersService>().ReverseMap();
        CreateMap<SpacificUserTaskDTO, SpacificUserTask>().ReverseMap();
        CreateMap<SeoServiceDetailsDTO, SeoServiceDetails>().ReverseMap();
        CreateMap<SeoTaskDetailsDto, SeoTaskDetails>().ReverseMap();
        CreateMap<AssignedUsersDTO, AssignedUsers>()
    .ForMember(dest => dest.Projects, opt => opt.Ignore()) // Ignoring navigation properties
    .ForMember(dest => dest.Users, opt => opt.Ignore())    // Ignoring navigation properties
    .ReverseMap();






    }
}

