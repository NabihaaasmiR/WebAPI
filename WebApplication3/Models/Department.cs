using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class Department
{
    public int DepartmentId { get; set; }

    [Required]
    [MaxLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Student> Students { get; set; } = new List<Student>();
}