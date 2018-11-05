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
using TestApiBakery.Services;

namespace TestApiBakery.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;

        public OrdersController(IOrderRepository orderRepository, IOrderService orderService)
        {
            _orderRepository = orderRepository;
            _orderService = orderService;
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
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("nip/{nip}")]
        public async Task<IActionResult> GetByNip(string nip)
        {
            var orders = await _orderService.GetByNipAsync(nip);
            if (orders.Count() == 0)
            {
                return NotFound();
            }
            return Ok(orders);
            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderAddDto orderAddDto)
        {
            if (orderAddDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (!await _orderRepository.CategoryExistsAsync(orderDto.CategoryId))
            //{
            //    return NotFound();
            //}
            await _orderService.AddAsync(orderAddDto);

            //var order = Mapper.Map<OrderCreateDto, Order>(orderCreateDto);
            // await _orderRepository.AddAsync(order);
            return StatusCode(201);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            if (orderUpdateDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (!await _orderRepository.CategoryExistsAsync(orderDto.CategoryId))
            //{
            //    return NotFound();
            //}
            await _orderService.UpdateAsync(id, orderUpdateDto);

            //var order = Mapper.Map<OrderCreateDto, Order>(orderCreateDto);
            // await _orderRepository.AddAsync(order);
            return StatusCode(201);
        }

    }
}
