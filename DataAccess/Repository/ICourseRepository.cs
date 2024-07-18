using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICourseRepository
    {
        void AddCourse(Course course);
        void DeleteCourse(Course course);
        Course GetCourseById(int id);
        List<Course> GetCourses();
        void UpdateCourse(Course course);
    }
}
