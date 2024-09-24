using System.ComponentModel.DataAnnotations;

namespace UsersApiBackend.Dto;

public class UserDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } 
    [Required]
    [EmailAddress]
    public string Email { get; set; } 
    [Required]
    [Range(0, 100)]
    public int Age { get; set; }
}