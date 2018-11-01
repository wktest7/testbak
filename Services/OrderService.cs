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
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository
            /*IHttpContextAccessor httpContextAccessor*/)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
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

            try
            {
                await _orderRepository.AddAsync(order);

            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
