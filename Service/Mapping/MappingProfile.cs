using AutoMapper;
using Domain.Entities;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Order, OrderDTO>();

            CreateMap<Product, ProductDTO>();
                                  
            CreateMap<User, UserDTO>();
            //CreateMap<UserDTO, User>();

            CreateMap<Review, ReviewDTO>();
            //CreateMap<ReviewDTO, Review>();
        }
    }
}
