namespace WebApplication3.DTOs;

public class DepartmentDto
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = string.Empty;

    public int StudentCount { get; set; }
}