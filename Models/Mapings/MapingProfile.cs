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
        //CreateMap<SocialMediaHandlingDTO, SocialMediaHandling>().ReverseMap();
        CreateMap<RegisterModel,Users>().ReverseMap();
        CreateMap<UserDTO, RegisterModel>().ReverseMap();
        CreateMap<UserWithClientsDto, Users>().ReverseMap(); 
        CreateMap<UserDTO, Users>().ReverseMap();
        CreateMap<ServicesDTO, Services>().ReverseMap();
        CreateMap<ProductsDTO, Products>().ReverseMap();
        CreateMap<WorkRecordsDTO, WorkRecords>().ReverseMap();
        CreateMap<OthersServiceDTO, OthersService>().ReverseMap();
        CreateMap<SeoServiceDetailsDTO, SeoServiceDetails>().ReverseMap();
        CreateMap<WorkRecordsSeoDetailsDTO, WorkRecordsSeoDetails>().ReverseMap();
        CreateMap<AssignedUsersDTO, AssignedUsers>()
    .ForMember(dest => dest.Projects, opt => opt.Ignore()) // Ignoring navigation properties
    .ForMember(dest => dest.Users, opt => opt.Ignore())    // Ignoring navigation properties
    .ReverseMap();






    }
}

