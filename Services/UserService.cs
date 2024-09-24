
using Microsoft.AspNetCore.Http.HttpResults;

namespace UsersApiBackend.Services;

using Microsoft.EntityFrameworkCore;
using UsersApiBackend.Models;
using UsersApiBackend.Context;
using UsersApiBackend.Dto;
using UsersApiBackend.Interface;

public class UserService : IUserService
{
    private readonly UserContext _context;

    public UserService(UserContext context)
    {
        _context = context;
    }

    private async Task<bool> UserExistsAsync(int id)
    {
        return await _context.Users.AnyAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateUserAsync(User user)
    {

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }

    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}