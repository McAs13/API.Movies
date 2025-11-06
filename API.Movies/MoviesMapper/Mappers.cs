using API.Movies.DAL.Models;
using API.Movies.DAL.Models.Dtos;
using AutoMapper;

namespace API.Movies.MoviesMapper
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
        }
    }
}
