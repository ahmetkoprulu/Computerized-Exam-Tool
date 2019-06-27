using Cet.Core.DataAccess.EntityFramework;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.Concrete.EntityFramework;
using Cet.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cet.DataAccess.Concrete.EntityFramework
{
    public class AnswerRepository
        : Repository<Answer, ApplicationDbContext>, IAnswerRepository
    {
        public void AddOrUpdate(Answer entity)
        {
            using(var context = new ApplicationDbContext())
            {
                var answer = context.Answers.SingleOrDefault(a => a.StudentId == entity.StudentId && a.QuestionId == entity.QuestionId);
                if (answer != null)
                {
                    context.Database.ExecuteSqlCommand(@"UPDATE Answers SET Text={0} WHERE StudentId={1} AND QuestionId={2}", entity.Text, entity.StudentId, entity.QuestionId);
                }
                else
                {
                    context.Database.ExecuteSqlCommand(@"INSERT INTO Answers(Text, Grade, QuestionId, StudentId) 
                                                         VALUES({0}, {1}, {2}, {3})", entity.Text, entity.Grade, entity.QuestionId, entity.StudentId);
                }
            }
        }

        public bool IsAnswerExist(int studentId, int questionId)
        {
            using (var context = new ApplicationDbContext())
            {
                var answer = context.Answers
                    .SingleOrDefault(a => a.StudentId == studentId && a.QuestionId == questionId);

                return answer != null;
            }
        }
    }
}