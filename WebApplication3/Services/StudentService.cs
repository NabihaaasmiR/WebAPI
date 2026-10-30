using WebApplication3.Models;

namespace WebApplication3.Services;

public class StudentService : IStudentService
{
    private readonly List<Student> _students = new()
    {
        new Student { Id = 1, Name = "Arun", Email = "arun@example.com", Age = 21 },
        new Student { Id = 2, Name = "Priya", Email = "priya@example.com", Age = 22 }
    };

    private int _nextId = 3;

    public List<Student> GetAllStudents() => _students;

    public Student? GetStudentById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public Student AddStudent(Student student)
    {
        student.Id = _nextId++;
        _students.Add(student);
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