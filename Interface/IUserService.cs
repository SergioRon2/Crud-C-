using UsersApiBackend.Dto;
using UsersApiBackend.Models;

namespace UsersApiBackend.Interface;

public interface IUserService
{ 
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}