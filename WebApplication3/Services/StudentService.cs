using Microsoft.Extensions.Logging;
using WebApplication3.Models;
using WebApplication3.DTOs; // Keep if needed for CreateStudentDto/UpdateStudentDto if parameters need them, but methods use Domain models

namespace WebApplication3.Services;

public class StudentService : IStudentService
{
    private readonly ILogger<StudentService> _logger;

    public StudentService(ILogger<StudentService> logger)
    {
        _logger = logger;
    }

    private readonly List<Student> _students = new()
    {
        new Student { Id = 1, Name = "Arun", Email = "arun@example.com", Age = 21 },
        new Student { Id = 2, Name = "Priya", Email = "priya@example.com", Age = 22 }
    };

    private int _nextId = 3;

    public List<Student> GetAllStudents()
    {
        return _students;
    }

    public Student? GetStudentById(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id);
    }

    public Student AddStudent(Student student)
    {
        _logger.LogInformation("Adding student with email {Email}", student.Email);

        student.Id = _nextId++;
        _students.Add(student);

        _logger.LogInformation("Student created successfully with ID {StudentId}", student.Id);

        return student;
    }

    public Student? UpdateStudent(int id, Student updatedStudent)
    {
        var student = GetStudentById(id);
        if (student == null) return null;

        student.Name = updatedStudent.Name;
        student.Email = updatedStudent.Email;
        student.Age = updatedStudent.Age;

        return student;
    }

    public bool DeleteStudent(int id)
    {
        var student = GetStudentById(id);
        if (student == null) return false;

        _students.Remove(student);
        return true;
    }

    public bool EmailExists(string email, int? excludeStudentId = null)
    {
        return _students.Any(s =>
            s.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
            && (!excludeStudentId.HasValue || s.Id != excludeStudentId.Value));
    }
}