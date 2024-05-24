using Electronic_E_commerce_Website_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Electronic_E_commerce_Website_API.DTO;
using Electronic_E_commerce_Website_API.Repository;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Electronic_E_commerce_Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly GenericRepository<Product> _rep;

        public ProductController(GenericRepository<Product> rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _rep.GetAll();
            var productDTOs = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                Description = p.Description, // Include Description property
                Quantity = p.Quantity,
                Rate = p.Rate
            }).ToList();

            return Ok(productDTOs);
        }

        [HttpPost]
        public IActionResult Add(ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Image = productDto.Image,
                Description = productDto.Description, // Include Description property
                Quantity = productDto.Quantity
            };

            _rep.Add(product);
            _rep.Save();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _rep.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Image = productDto.Image;
            product.Description = productDto.Description; // Include Description property
            product.Quantity = productDto.Quantity;

            _rep.Update(product);
            _rep.Save();

            return NoContent();
        }




        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _rep.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Image = product.Image,
                Rate = product.Rate,
                Description = product.Description, // Include Description property
                Quantity = product.Quantity
            };

            return Ok(productDto);
        }


        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var product = _rep.GetById(id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    _rep.Delete(id);
        //    _rep.Save();

        //    return NoContent();
        //}




        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _rep.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            try
            {
                _rep.Delete(id);
                _rep.Save();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                // Check if the exception is due to a constraint violation
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    // The DELETE statement conflicted with the REFERENCE constraint
                    // Return a meaningful error response to the client
                    return BadRequest("This product cannot be deleted because it has associated order details.");
                }

                // If it's another type of exception, handle it accordingly
                // Log the exception or return a generic error response
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}

