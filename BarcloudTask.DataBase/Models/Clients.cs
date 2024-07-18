using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BarcloudTask.DataBase.Models;

[Index(nameof(Email), IsUnique = true)]
public class Client
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public required string FirstName { get; set; }
    [MaxLength(50)]
    public required string LastName { get; set; }
    [MaxLength(50)]
    public required string Email { get; set; }
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
}
