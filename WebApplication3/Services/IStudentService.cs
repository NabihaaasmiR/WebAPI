using WebApplication3.DTOs;
using WebApplication3.Models;

namespace WebApplication3.Services;

public interface IStudentService
{
    List<StudentDto> GetAllStudents();

    StudentDto? GetStudentById(int id);

    StudentDto AddStudent(CreateStudentDto studentDto);

    StudentDto? UpdateStudent(int id, UpdateStudentDto studentDto);

    bool DeleteStudent(int id);

    bool EmailExists(string email, int? id = null);
}