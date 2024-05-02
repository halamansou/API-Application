using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Electronic_E_commerce_Website_API.Repository;
using Electronic_E_commerce_Website_API.Models;
using Electronic_E_commerce_Website_API.DTO;
namespace Electronic_E_commerce_Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        GenericRepository<User> rep;

        public UsersController(GenericRepository<User> rep)
        {

            this.rep = rep;
        }

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var users= rep.GetAll();

        //    return Ok(users);

        //}



        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> users = rep.GetAll();
            List<UserDTO> usersDto = new List<UserDTO>();

            foreach (User user in users)
            {
                UserDTO userDto = new UserDTO()
                {
                    Id = user.Id,
                    Name = user.Username,
                    Address = user.Address,
                    Phone = user.Phone,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role

                };
                usersDto.Add(userDto);
            }
            return Ok(usersDto);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            
            User user = rep.GetById(id);

            if(user == null)
            {
                return NotFound();
            }
            else
            {
                UserDTO userDto = new UserDTO()
                {
                    Id = user.Id,
                    Name = user.Username,
                    Address = user.Address,
                    Phone = user.Phone,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role

                };
                return Ok(userDto);

            }
        
        
        }


        [HttpPost]
        public IActionResult Add(UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User
            {
                Username = userDto.Name,
                Address = userDto.Address,
                Phone = userDto.Phone,
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role
            };

            rep.Add(user);
            rep.Save();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = rep.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Username = userDto.Name;
            user.Address = userDto.Address;
            user.Phone = userDto.Phone;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.Role = userDto.Role;

            rep.Update(user);
            rep.Save();

            return NoContent();
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = rep.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            rep.Delete(id);
            rep.Save();

            return NoContent();
        }

    }
}
