using System.Collections.Generic;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class QuestionType : IEntity
    {
        public QuestionType()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}