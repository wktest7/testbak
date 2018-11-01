using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApiBakery.Data;
using TestApiBakery.Data.Repositories;
using TestApiBakery.Models;

namespace TestApiBakery.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Order, OrderDto>(order));
            //return Ok(order);
        }

        [HttpGet("nip/{nip}")]
        public async Task<IActionResult> GetByNip(string nip)
        {
            var orders = await _orderRepository.GetByNipAsync(nip);
            if (orders.Count() == 0)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders));
            
        }

        // POST: api/Orders
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
