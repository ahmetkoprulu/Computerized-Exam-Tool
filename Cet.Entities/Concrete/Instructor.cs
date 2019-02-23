using System.Collections.Generic;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class Instructor : IRegistrable
    {
        public Instructor()
        {
            CourseOfferings = new HashSet<CourseOffering>();
        }

        public int Id { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CourseOffering> CourseOfferings { get; set; }
    }
}