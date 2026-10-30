using WebApplication3.DTOs;

namespace WebApplication3.Services;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllStudentsAsync();
    Task<StudentDto?> GetStudentByIdAsync(int id);
    Task<StudentDto> CreateStudentAsync(CreateStudentDto createDto);
    Task<bool> UpdateStudentAsync(int id, UpdateStudentDto updateDto);
    Task<bool> DeleteStudentAsync(int id);
}