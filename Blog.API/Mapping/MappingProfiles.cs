using AutoMapper;
using Blog.Domain.Models;
using Blog.Services.Dtos;

namespace Blog.API.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BlogPost, BlogNode>();
            CreateMap<BlogNode, BlogPost>();
        }
    }
}
