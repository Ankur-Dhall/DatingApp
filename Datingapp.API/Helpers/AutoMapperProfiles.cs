using System.Linq;
using AutoMapper;
using Datingapp.API.Dtos;
using Datingapp.API.Models;

namespace Datingapp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt => 
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.Ismain).Url))
                .ForMember(dest => dest.Age , opt => 
                    opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => 
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.Ismain).Url))
                .ForMember(dest => dest.Age , opt => 
                    opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
                    
            CreateMap<Photo, PhotoForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserToRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt =>
                    opt.MapFrom(p => p.Sender.Photos.FirstOrDefault(p => p.Ismain).Url))
                .ForMember(m => m.RecipientPhotoUrl, opt =>
                    opt.MapFrom(p => p.Recipient.Photos.FirstOrDefault(p => p.Ismain).Url));
        }
    }
}