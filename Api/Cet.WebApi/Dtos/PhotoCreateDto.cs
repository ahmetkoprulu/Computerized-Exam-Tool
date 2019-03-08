using Microsoft.AspNetCore.Http;

namespace Cet.WebApi.Dtos
{
    public class PhotoCreateDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string PhotoId { get; set; }
    }
}