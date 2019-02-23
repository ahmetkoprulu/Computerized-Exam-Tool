using System.Collections.Generic;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class Question : IEntity
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int Num { get; set; }
        public int ExamId { get; set; }
        public int QuestionTypeId { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual QuestionType QuestionType { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}