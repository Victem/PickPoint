using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PickPoint.Data.DB;
using PickPoint.Data.DTO;
using PickPoint.Data.DTOMappings;
using PickPoint.Data.Entities;
using PickPoint.Data.Enums;
using PickPoint.Infrastructure;

namespace PickPoint.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Postamate, string> _postamateRepository;
        private readonly IRepository<Order, int> _orderRepository;

        public OrderController(IRepository<Postamate, string> postamateRepository, IRepository<Order, int> orderRepository)
        {
            _postamateRepository = postamateRepository;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public ActionResult<Order> Post(OrderCreateDto order)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState);}
            
            var postamate = _postamateRepository.FindById(order.PostamateId);
            if (postamate == null)
            {
                return NotFound();
            }

            if (!postamate.Status)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var newOrder = order.ToOrder();
            _orderRepository.Create(newOrder);

            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder.ToOrderDto());
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDto> Get(int id)
        {
            var order = _orderRepository.FindById(id);

            if (order == null) 
            {
                return NotFound();
            }

            return Ok(order.ToOrderDto());
        }

        [HttpPut]
        public ActionResult Put(OrderUpdateDto order)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            
            var existedOrder = _orderRepository.FindById(order.Id);
            if (existedOrder == null)
            {
                return NotFound();
            }

            existedOrder.FromUpdateDto(order);
            _orderRepository.Update(existedOrder);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var order = _orderRepository.FindById(id);
            if (order == null) { return NotFound(); }
            
            _orderRepository.Remove(order);
            return NoContent();
        }
    }
}
