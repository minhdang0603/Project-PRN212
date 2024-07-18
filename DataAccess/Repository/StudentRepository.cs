using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public List<Student> GetStudents() => StudentDAO.Instance.GetStudents();
        public Student GetStudentById(int id) => StudentDAO.Instance.GetStudentById(id);
        public void AddStudent(Student stundent) => StudentDAO.Instance.Add(stundent);
        public void UpdateStudent(Student student) => StudentDAO.Instance.Update(student);
        public void DeleteStudent(Student student) => StudentDAO.Instance.Delete(student);
        public List<Student> FindStudentByName(string name) => StudentDAO.Instance.FindStudentByName(name);
    }
}
