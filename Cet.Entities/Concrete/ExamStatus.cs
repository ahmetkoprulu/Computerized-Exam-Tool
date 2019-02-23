using System.Collections.Generic;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class ExamStatus : IEntity
    {
        public ExamStatus()
        {
            Exams = new HashSet<Exam>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}