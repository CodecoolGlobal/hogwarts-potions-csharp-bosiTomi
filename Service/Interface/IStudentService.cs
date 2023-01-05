using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Service.Interface;

public interface IStudentService
{
    Task AddStudent(Student student);
    Task<Student> GetStudent(long studentId);
    Task<Student> GetStudentByUsername(string username);
    Task<List<Student>> GetAllStudents();
    Task UpdateStudent(Student student);
    Task DeleteStudent(long id);
    bool ValidateLogin(LoginForm user);
    bool Register(Student user);
}