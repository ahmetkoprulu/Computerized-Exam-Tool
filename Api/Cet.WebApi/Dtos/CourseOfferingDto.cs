namespace Cet.WebApi.Dtos
{
    public class CourseOfferingDto
    {
        public int Id { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }
        public bool? IsActive { get; set; }
        public int CourseId { get; set; }
    }
}