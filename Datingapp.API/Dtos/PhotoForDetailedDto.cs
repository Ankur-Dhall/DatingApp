using System;

namespace Datingapp.API.Dtos
{
    public class PhotoForDetailedDto //T get Photos when someone views a profile.
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Ismain { get; set; }      
    }
}