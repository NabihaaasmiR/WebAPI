using WebApplication3.DTOs;

namespace WebApplication3.Services;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllStudentsAsync();

    Task<StudentDto?> GetStudentByIdAsync(int id);

    Task<bool> EmailExistsAsync(string email, int? excludeStudentId = null);

    Task<StudentDto> AddStudentAsync(CreateStudentDto createStudentDto);

    Task<StudentDto?> UpdateStudentAsync(int id, UpdateStudentDto updateStudentDto);

    Task<bool> DeleteStudentAsync(int id);
}