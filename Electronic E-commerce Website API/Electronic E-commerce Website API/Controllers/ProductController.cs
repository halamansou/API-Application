//using Electronic_E_commerce_Website_API.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Electronic_E_commerce_Website_API.DTO;
//using Electronic_E_commerce_Website_API.Repository;
//namespace Electronic_E_commerce_Website_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductController : ControllerBase
//    {
//        GenericRepository<Product> rep;
//        public ProductController(GenericRepository<Product> rep)
//        {
//            this.rep = rep;
//        }

//        //[HttpGet]
//        //public IActionResult GetAll()
//        //{
//        //    var products = rep.GetAll();

//        //    return Ok(products);

//        //}
//    }
//}

using Electronic_E_commerce_Website_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Electronic_E_commerce_Website_API.DTO;
using Electronic_E_commerce_Website_API.Repository;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

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
                Quantity = p.Quantity
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
            product.Quantity = productDto.Quantity;

            _rep.Update(product);
            _rep.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _rep.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            _rep.Delete(id);
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
                Quantity = product.Quantity
            };

            return Ok(productDto);
        }
    }
}

