using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiBakery.Data;
using TestApiBakery.Data.Repositories;
using TestApiBakery.Models;

namespace TestApiBakery.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository
            /*IHttpContextAccessor httpContextAccessor*/, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            //_httpContextAccessor = httpContextAccessor;
            //i potem user manager get by name
        }

        public async Task AddAsync(OrderCreateDto orderCreateDto)
        {
            //var user = _httpContextAccessor.HttpContext.User.Identity.Name
            var userId = "3102a2df-fa72-4458-9fe0-f94d84dc6a82";
            var order = new Order();
            order.AppUserId = userId;


            foreach (var item in orderCreateDto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with id: '{item.ProductId}' not exists.");
                }
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }


            await _orderRepository.AddAsync(order);


        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<Order, OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetByNipAsync(string nip)
        {
            var orders = await _orderRepository.GetByNipAsync(nip);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
        }

    }
}
