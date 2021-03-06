using System;
using Microsoft.AspNetCore.Http;

namespace Datingapp.API.Dtos
{
    public class PhotoForCreationDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string PublicId { get; set; }

        public PhotoForCreationDto()
        {
            DateCreated = DateTime.Now;
        }
    }
}