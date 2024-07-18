using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IScoreRepository
    {
        void AddScore(Score score);
        void DeleteScore(Score score);
        Score GetScoreByCourseIdAndStudentId(int courseId, int studentId);
        List<Score> GetScoresByCourse(int courseId);
        void UpdateScore(Score score);
    }
}
