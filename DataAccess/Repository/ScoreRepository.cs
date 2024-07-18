using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        public List<Score> GetScoresByCourse(int courseId) => ScoreDAO.Instance.GetScoresByCourses(courseId);
        public Score GetScoreByCourseIdAndStudentId(int courseId, int studentId) => ScoreDAO.Instance.GetScoreByCourseIdAndStudentId(courseId, studentId);
        public void AddScore(Score score) => ScoreDAO.Instance.Add(score);
        public void UpdateScore(Score score) => ScoreDAO.Instance.Update(score);
        public void DeleteScore(Score score) => ScoreDAO.Instance.Delete(score);
    }
}
