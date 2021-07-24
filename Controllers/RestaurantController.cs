using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.IRepository;
using TodoApp.Models;
using TodoApp.Models.DTOs;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _repository;
        private readonly IMapper _mapper;
        public RestaurantController(IRestaurantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetAllItem")]
        public async Task<IActionResult> GetAllItem()
        {
            var items = await _repository.GetAllItem();
            return Ok(items);
        }
        
        [HttpGet("GetAllIOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            var items = await _repository.GetAllOrder();
            return Ok(items);
        }
        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var items = await _repository.GetAllCustomer();
            return Ok(items);
        }

        [HttpPost("AddOrEditOrder")]
        public async Task<IActionResult> AddOrEditOrder([FromBody]OrderDto order)
        {
            if (ModelState.IsValid)
            {
                //var orderSaving = _mapper.Map<Order>(order);
                var orders = _repository.GetAllOrder().Result.OrderByDescending(x => x.CreatedDate);
                var newOrderId = orders.FirstOrDefault()?.OrderId + 1 ?? 1;
                var orderSaving = new Order()
                {
                    OrderId =  newOrderId,
                    CreatedDate = DateTime.Now,
                    GTotal = order.GTotal,
                    OrderNo =  order.OrderNo,
                    PMethod =  order.PMethod,
                    Customer = _repository.GetAllCustomer().Result.FirstOrDefault(x=>x.CustomerId == order.CustomerId)
                };
                
                var orderItems = _repository.GetAllOrderItem().Result.OrderByDescending(x=>x.CreatedDate);
                var newOrderItemId = orderItems.FirstOrDefault()?.OrderItemId + 1 ?? 1;
                var orderItemsSaving = order.OrderItems.Select((x, i) => new OrderItem()
                {
                    Order = orderSaving,
                    Quantity = x.Quantity,
                    OrderItemId = newOrderItemId + i,
                    CreatedDate = DateTime.Now,
                    Item =  _repository.GetAllItem().Result.FirstOrDefault(i=>i.ItemId==x.ItemId)
                });
                
                orderSaving.OrderItems = orderItemsSaving.ToList();
                try
                {
                    await _repository.AddOrEditOrder(orderSaving);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}