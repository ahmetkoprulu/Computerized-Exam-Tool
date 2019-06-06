using Cet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cet.Entities.Concrete
{
    public class StudentActivities: IEntity
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
    }
}
