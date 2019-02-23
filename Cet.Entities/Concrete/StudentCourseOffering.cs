using System;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class StudentCourseOffering : IEntity
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int StudentId { get; set; }
        public int CourseOfferingId { get; set; }

        public virtual CourseOffering CourseOffering { get; set; }
        public virtual Student Student { get; set; }
    }
}