using System;

namespace Datingapp.API.Dtos
{
    public class PhotoForReturnDto // To get the uploaded Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Ismain { get; set; }
        public string PublicId { get; set; }
        
    }
}