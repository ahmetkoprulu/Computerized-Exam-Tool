using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class Answer : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Grade { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }

        public virtual Question Question { get; set; }
        public virtual Student Student { get; set; }
    }
}