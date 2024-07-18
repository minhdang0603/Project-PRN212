using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IStudentRepository
    {
        void AddStudent(Student stundent);
        void DeleteStudent(Student student);
        List<Student> FindStudentByName(string name);
        Student GetStudentById(int id);
        List<Student> GetStudents();
        void UpdateStudent(Student student);
    }
}
