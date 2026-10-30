using WebApplication3.Models;

namespace WebApplication3.Services;

public interface IStudentService
{
    List<Student> GetAllStudents();
    Student? GetStudentById(int id);
    Student AddStudent(Student student);
    Student? UpdateStudent(int id, Student updatedStudent);
    bool DeleteStudent(int id);
}