using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CourseRepository : ICourseRepository
    {
        public List<Course> GetCourses() => CourseDAO.Instance.GetCourses();
        public Course GetCourseById(int id) => CourseDAO.Instance.GetCourseById(id);
        public void AddCourse(Course course) => CourseDAO.Instance.Add(course);
        public void UpdateCourse(Course course) => CourseDAO.Instance.Update(course);
        public void DeleteCourse(Course course) => CourseDAO.Instance.Delete(course);
    }
}
