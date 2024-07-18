using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class CourseDAO
    {
        private static CourseDAO instance = null;
        private static object instanceLock = new object();
        private static StudentManagementContext context;
        private CourseDAO() { }
        public static CourseDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CourseDAO();
                        context = new StudentManagementContext();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------------
        public List<Course> GetCourses()
        {
            var list = new List<Course>();
            try
            {
                list = context.Courses.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        //-------------------------------------------
        public Course GetCourseById(int id)
        {
            var course = new Course();
            try
            {
                course = context.Courses.Include(c => c.Scores).FirstOrDefault(c => c.CourseId == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return course;
        }
        //-------------------------------------------
        public void Add(Course course)
        {
            try
            {
                context.Courses.Add(course);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------
        public void Update(Course course)
        {
            try
            {
                context.Courses.Update(course);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------
        public void Delete(Course course)
        {
            try
            {
                context.Courses.Remove(course);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
