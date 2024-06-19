using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Electronic_E_commerce_Website_API.Repository;
using Electronic_E_commerce_Website_API.Models;
using Electronic_E_commerce_Website_API.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        public IActionResult Add(RegesterDTO regesterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User
            {
                Username = regesterDTO.Name,
                Address = regesterDTO.Address,
                Phone = regesterDTO.Phone,
                Email = regesterDTO.Email,
                Password = regesterDTO.Password,
                Role = "User"
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






        //////login without Using JWT




        //[HttpPost("login")]
        //public IActionResult Login(LoginDTO loginDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // Check if user exists with the provided email
        //    var user = rep.GetAll().FirstOrDefault(u => u.Email == loginDto.Email);

        //    if (user == null)
        //    {
        //        return NotFound("User not found");
        //    }

        //    // Check if the password matches
        //    if (user.Password != loginDto.Password)
        //    {
        //        return BadRequest("Incorrect password");
        //    }

        //    // Return user details
        //    var userDto = new UserDTO()
        //    {
        //        Id = user.Id,
        //        Name = user.Username,
        //        Address = user.Address,
        //        Phone = user.Phone,
        //        Email = user.Email,
        //        Role = user.Role
        //    };

        //    return Ok(userDto);
        //}









        //////login Using JWT

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if user exists with the provided email
            var user = rep.GetAll().FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Check if the password matches
            if (user.Password != loginDto.Password)
            {
                return BadRequest("Incorrect password");
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("My Secret Key Generate By Hala Mansour Ali");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
                    // Add more claims as needed
                }),
                Expires = DateTime.UtcNow.AddDays(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return user details along with the token
            var userDto = new UserDTO()
            {
                Id = user.Id,
                Name = user.Username,
                Address = user.Address,
                Phone = user.Phone,
                Email = user.Email,
                Role = user.Role
            };

            return Ok(new { User = userDto, Token = tokenString });
        }

    }
}
