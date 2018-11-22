using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestApiBakery.Data;
using TestApiBakery.Data.Repositories;
using TestApiBakery.Mappers;
using TestApiBakery.Models;
using TestApiBakery.Services;

namespace TestApiBakery
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }

                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 403;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddDbContext<BakeryDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>()
               .AddEntityFrameworkStores<BakeryDbContext>()
               .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,
                    ValidateIssuer = true,
                    ValidateAudience = true
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //AutoMapper.Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<Product, ProductDto>()
            //    .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Name))
            //    .ReverseMap()
            //    .ForPath(dest => dest.Category.Name, opts => opts.MapFrom(src => src.Category))
            //    .ForPath(dest => dest.Category.CategoryId, opts => opts.MapFrom(src => src.CategoryId));

            //    cfg.CreateMap<OrderItem, OrderItemDto>()
            //    .ForMember(dest => dest.ProductName, opts => opts.MapFrom(src => src.Product.Name))
            //    .ForMember(dest => dest.Weight, opts => opts.MapFrom(src => src.Product.Weight))
            //    .ForMember(dest => dest.ProductPrice, opts => opts.MapFrom(src => src.Product.Price))
            //    .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Product.Price * src.Quantity));

            //    cfg.CreateMap<Order, OrderDto>()
            //    //.ForMember(dest => dest.CompanyName, opts => opts.MapFrom(src => src.BakeryDetails.Name))
            //    //.ForMember(dest => dest.CompanyAddress, opts => opts.MapFrom(src => src.BakeryDetails.Address))
            //    //.ForMember(dest => dest.CompanyPostalCode, opts => opts.MapFrom(src => src.BakeryDetails.PostalCode))
            //    //.ForMember(dest => dest.CompanyNip, opts => opts.MapFrom(src => src.BakeryDetails.Nip))
            //    //.ForMember(dest => dest.CompanyPhone, opts => opts.MapFrom(src => src.BakeryDetails.Phone))
            //    //.ForMember(dest => dest.CustomerName, opts => opts.MapFrom(src => src.AppUser.CompanyName))
            //    //.ForMember(dest => dest.CustomerAddress, opts => opts.MapFrom(src => src.AppUser.Address))
            //    //.ForMember(dest => dest.CustomerPostalCode, opts => opts.MapFrom(src => src.AppUser.PostalCode))
            //    //.ForMember(dest => dest.CustomerNip, opts => opts.MapFrom(src => src.AppUser.Nip))
            //    //.ForMember(dest => dest.CustomerPhone, opts => opts.MapFrom(src => src.AppUser.PhoneNumber))
            //    //.ForMember(dest => dest.OrderItems, opts => opts.MapFrom(src => src.OrderItems))
            //    .ForMember(dest => dest.FinalPrice, opts => opts.MapFrom(src => src.OrderItems.Sum(x => x.Product.Price * x.Quantity)));
            //    //.AfterMap((src, dest) => dest.FinalPrice = src.OrderItems.Sum(x => x.Product.Price * x.Quantity));


            //});
            app.UseCors(builder =>
               builder.WithOrigins("http://localhost:8080")
               .AllowAnyHeader()
               .AllowAnyMethod());

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
  
