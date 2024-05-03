using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Electronic_E_commerce_Website_API.Repository;
using Electronic_E_commerce_Website_API.Models;
using Electronic_E_commerce_Website_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electronic_E_commerce_Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly GenericRepository<Order> repo;

        public OrderController(GenericRepository<Order> repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Order> orders = repo.GetAll();
            List<OrderDTO> ordersDto = orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                UserId = o.UserId ?? 0,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                OrderDate = o.OrderDate ?? DateTime.MinValue // Handle nullable DateTime
            }).ToList();

            return Ok(ordersDto);
        }



        [HttpPost]
        public IActionResult Create(OrderDTO orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest();
            }

            Order order = new Order
            {
                UserId = orderDto.UserId,
                Status = orderDto.Status,
                TotalAmount = orderDto.TotalAmount,
                OrderDate = orderDto.OrderDate
            };

            repo.Add(order);
            repo.Save();

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, OrderDTO orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest();
            }

            Order order = repo.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            order.UserId = orderDto.UserId;
            order.Status = orderDto.Status;
            order.TotalAmount = orderDto.TotalAmount;
            order.OrderDate = orderDto.OrderDate;

            repo.Update(order);
            repo.Save();

            return NoContent();
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Order order = repo.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            repo.Delete(id);
            repo.Save();

            return NoContent();
        }




        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Order order = repo.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            OrderDTO orderDto = new OrderDTO
            {
                Id = order.Id,
                UserId = order.UserId ?? 0,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate ?? DateTime.MinValue 
            };

            return Ok(orderDto);
        }
    }
}
