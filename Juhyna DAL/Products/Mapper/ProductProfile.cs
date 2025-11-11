using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Products.Mapper
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.Product, Juhyna_DAL.Products.Dto.DTOProducctRead>().
                ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID));
            CreateMap<Models.Product, Juhyna_DAL.Products.Dto.DTOproductAdd>()
              .ForMember(dest => dest.size, opt => opt.MapFrom(src => src.Size))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
              .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID));
        }
        }
}
