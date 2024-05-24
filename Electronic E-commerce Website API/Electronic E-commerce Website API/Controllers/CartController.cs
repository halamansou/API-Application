using Electronic_E_commerce_Website_API.Models;
using Electronic_E_commerce_Website_API.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Electronic_E_commerce_Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork;

        public CartController(UnitOfWork.UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/cart/user/{userId}
        [HttpGet("user/{userId}")]
        public IActionResult GetCartByUserId(int userId)
        {
            try
            {
                // Find the user's cart based on the user ID
                var cart = _unitOfWork.CartRepository.Get(c => c.UserId == userId).FirstOrDefault();

                if (cart == null)
                {
                    return NotFound("Cart not found for the user.");
                }

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
