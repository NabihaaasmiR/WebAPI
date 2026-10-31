using WebApplication3.DTOs;

namespace WebApplication3.Services;

public interface IDepartmentService
{
    Task<List<DepartmentDto>> GetAllDepartmentsAsync();

    Task<DepartmentDto?> GetDepartmentByIdAsync(int id);

    Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto);

    Task<DepartmentDto?> UpdateDepartmentAsync(int id, CreateDepartmentDto dto);

    Task<DepartmentDeleteResult> DeleteDepartmentAsync(int id);
}

public enum DepartmentDeleteResult
{
    NotFound,
    HasStudents,
    Deleted
}