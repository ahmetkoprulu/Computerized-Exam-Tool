﻿using System;
using System.Collections.Generic;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class Exam : IEntity
    {
        public Exam()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseOfferingId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int ExamStatusId { get; set; }

        public virtual CourseOffering CourseOffering { get; set; }
        public virtual ExamStatus ExamStatus { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}