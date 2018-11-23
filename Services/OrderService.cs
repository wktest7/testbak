using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository,
            IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<AppUser> userManager)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task AddAsync(OrderAddDto orderAddDto)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            var order = new Order();
            order.AppUserId = userId;
            order.Status = Status.New;
            order.DateCreated = DateTime.Now;

            foreach (var item in orderAddDto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null || product.IsHidden == true )
                {
                    throw new Exception($"Product with id: '{item.ProductId}' not exists.");
                }
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price    
                });
            }
            await _orderRepository.AddAsync(order);
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
        }


        public async Task<IEnumerable<OrderDto>> GetByUserIdAsync()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);
        }

        public async Task UpdateAsync(OrderUpdateDto orderUpdateDto)
        {
            var order = await _orderRepository.GetByIdAsync(orderUpdateDto.OrderId);
            if (order == null)
            {
                throw new Exception($"Order with id: '{orderUpdateDto.OrderId}' not exists.");
            }
            if (orderUpdateDto.Status == Status.Shipped)
            {
                order.Status = orderUpdateDto.Status;
            }
    
            await _orderRepository.UpdateAsync(order);
        }
    }
}
