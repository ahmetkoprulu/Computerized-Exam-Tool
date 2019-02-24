using System.Collections.Generic;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class Course : IEntity
    {
        public Course()
        {
            CourseOfferings = new HashSet<CourseOffering>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<CourseOffering> CourseOfferings { get; set; }
    }
}