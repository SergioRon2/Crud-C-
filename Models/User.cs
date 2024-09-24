using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace UsersApiBackend.Models;

[Table("Users")]
public class User
{
    public int Id { get; set; } 
    [Required]
    public string Name { get; set; } 
    [Required]
    public string Email { get; set; }
    [Required]
    public int Age { get; set; }
}

