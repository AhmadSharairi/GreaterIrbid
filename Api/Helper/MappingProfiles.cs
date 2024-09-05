
using Api.Dtos;
using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<NewsArticle, NewsArticleDto>()
             .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
             .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<NewsArticleDto, NewsArticle>()
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl != null ? src.ImageUrl.FileName : null))
               .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<EnvironmentImage, UploadImageDto>();
            CreateMap<UploadImageDto, EnvironmentImage>();

            CreateMap<AdviceImage, UploadImageDto>();
            CreateMap<UploadImageDto, AdviceImage>();

            CreateMap<ComplaintDto, Complaint>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow));


            CreateMap<CitizenSatisfactionDto, CitizenSatisfaction>()
                       .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()); 

            CreateMap<CitizenSatisfaction, CitizenSatisfactionDto>();




        }

    
    }}