using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class StudentDAO
    {
        private static StudentDAO instance = null;
        private static object instanceLock = new object();
        private static StudentManagementContext context;
        private StudentDAO() { }
        public static StudentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StudentDAO();
                        context = new StudentManagementContext();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------------
        public List<Student> GetStudents()
        {
            var list = new List<Student>();
            try
            {
                list = context.Students.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        //-------------------------------------------
        public Student GetStudentById(int id)
        {
            var student = new Student();
            try
            {
                student = context.Students.Include(s => s.Scores).FirstOrDefault(s => s.StudentId == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return student;
        }
        //-------------------------------------------
        public void Add(Student student)
        {
            try
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------
        public void Update(Student student)
        {
            try
            {
                context.Students.Update(student);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------
        public void Delete(Student student)
        {
            try
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------
        public List<Student> FindStudentByName(string name)
        {
            var student = new List<Student>();
            try
            {
                student = context.Students.Where(s => s.FirstName.ToLower().Contains(name.ToLower()) || s.LastName.ToLower().Contains(name.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return student;
        }
    }
}
