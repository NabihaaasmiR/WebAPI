using System.ComponentModel.DataAnnotations;

namespace WebApplication3.DTOs;

public class CreateDepartmentDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string DepartmentName { get; set; } = string.Empty;
}