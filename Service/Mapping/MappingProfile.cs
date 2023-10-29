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
            CreateMap<OrderDTO, Order>();
              //  .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.UserId));

            CreateMap<OrderCreationDTO, Order>();


            CreateMap<Product, ProductDTO>();
                                  
            CreateMap<User, UserDTO>();
            //CreateMap<UserDTO, User>();

            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewDTO, Review>();
        }
    }
}
