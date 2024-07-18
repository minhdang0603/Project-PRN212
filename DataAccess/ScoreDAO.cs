using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class ScoreDAO
    {
        private static ScoreDAO instance = null;
        private static object instanceLock = new object();
        private static StudentManagementContext context;
        private ScoreDAO() { }
        public static ScoreDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ScoreDAO();
                        context = new StudentManagementContext();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------------
        public List<Score> GetScoresByCourses(int courseId)
        {
            var list = new List<Score>();
            try
            {
                list = context.Scores.Include(sc => sc.Student).Include(sc => sc.Course).Where(sc => sc.CourseId == courseId).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        //-------------------------------------------
        public Score GetScoreByCourseIdAndStudentId(int courseId, int studentId)
        {
            var score = new Score();
            try
            {
                score = context.Scores.Include(sc => sc.Student).Include(sc => sc.Course).FirstOrDefault(sc => sc.CourseId == courseId && sc.StudentId == studentId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return score;
        }
        //-------------------------------------------
        public void Add(Score score)
        {
            try
            {
                context.Scores.Add(score);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------
        public void Update(Score score)
        {
            try
            {
                context.Scores.Update(score);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------
        public void Delete(Score score)
        {
            try
            {
                context.Scores.Remove(score);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
