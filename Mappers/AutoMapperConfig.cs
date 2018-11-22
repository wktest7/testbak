using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Data;
using TestApiBakery.Models;

namespace TestApiBakery.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Name));
               // .ForMember(dest => dest.CategoryId, opts => opts.NullSubstitute("testtest"));
                // .ReverseMap()
                //.ForPath(dest => dest.Category.Name, opts => opts.MapFrom(src => src.Category))
                //.ForPath(dest => dest.Category.CategoryId, opts => opts.MapFrom(src => src.CategoryId));

                cfg.CreateMap<ProductUpdateDto, Product>()
                //.ForPath(dest => dest.Category.Name, opts => opts.MapFrom(src => src.Category))
                .ForPath(dest => dest.Category.CategoryId, opts => opts.MapFrom(src => src.CategoryId));

                cfg.CreateMap<ProductAddDto, Product>()
                .ForPath(dest => dest.Category.CategoryId, opts => opts.MapFrom(src => src.CategoryId));

                cfg.CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opts => opts.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Weight, opts => opts.MapFrom(src => src.Product.Weight))
                .ForMember(dest => dest.ProductPrice, opts => opts.MapFrom(src => src.Price))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price * src.Quantity));

                cfg.CreateMap<Order, OrderDto>()
                .ForSourceMember(dest => dest.Status, opts => opts.Ignore())
                .ForMember(dest => dest.FinalPrice, opts => opts.MapFrom(src => src.OrderItems.Sum(x => x.Price * x.Quantity)))
                .ForMember(dest => dest.CompanyName, opts => opts.MapFrom(src => src.AppUser.CompanyName))
                .ForMember(dest => dest.Nip, opts => opts.MapFrom(src => src.AppUser.Nip));

                cfg.CreateMap<CategoryDto, Category>();

                //.ForMember(dest => dest.CompanyName, opts => opts.MapFrom(src => src.BakeryDetails.Name))
                //.ForMember(dest => dest.CompanyAddress, opts => opts.MapFrom(src => src.BakeryDetails.Address))
                //.ForMember(dest => dest.CompanyPostalCode, opts => opts.MapFrom(src => src.BakeryDetails.PostalCode))
                //.ForMember(dest => dest.CompanyNip, opts => opts.MapFrom(src => src.BakeryDetails.Nip))
                //.ForMember(dest => dest.CompanyPhone, opts => opts.MapFrom(src => src.BakeryDetails.Phone))
                //.ForMember(dest => dest.CustomerName, opts => opts.MapFrom(src => src.AppUser.CompanyName))
                //.ForMember(dest => dest.CustomerAddress, opts => opts.MapFrom(src => src.AppUser.Address))
                //.ForMember(dest => dest.CustomerPostalCode, opts => opts.MapFrom(src => src.AppUser.PostalCode))
                //.ForMember(dest => dest.CustomerNip, opts => opts.MapFrom(src => src.AppUser.Nip))
                //.ForMember(dest => dest.CustomerPhone, opts => opts.MapFrom(src => src.AppUser.PhoneNumber))
                //.ForMember(dest => dest.OrderItems, opts => opts.MapFrom(src => src.OrderItems))
                //.AfterMap((src, dest) => dest.FinalPrice = src.OrderItems.Sum(x => x.Product.Price * x.Quantity));


            })
            .CreateMapper();
    }
}
