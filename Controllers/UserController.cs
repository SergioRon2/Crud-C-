using Microsoft.AspNetCore.Mvc;
using UsersApiBackend.Dto;
using UsersApiBackend.Interface;
using UsersApiBackend.Models;
using UsersApiBackend.Services;

namespace UsersApiBackend.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            var newUser = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Age = userDto.Age,
            };
            
            Console.WriteLine($"New user: {newUser.Name}, {newUser.Email}, {newUser.Age}");
            
            await _userService.CreateUserAsync(newUser);
            try
            {
                return newUser;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            var userExisted =  await _userService.GetUserByIdAsync(id);

            if (userExisted != null)
            {
                userExisted.Name = userDto.Name;
                userExisted.Email = userDto.Email;
                userExisted.Age = userDto.Age;
            }
            else
            {
                return NotFound();
            }
            
            var updated = await _userService.UpdateUserAsync(userExisted);
            if (!updated)
            {
                return NotFound();
            }
            return Ok($"user updated: {userExisted}");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return result ? Ok(result) : NotFound();
        }
    }
