using System;

namespace Cet.WebApi.Dtos
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int ExamStatusId { get; set; }
    }
}