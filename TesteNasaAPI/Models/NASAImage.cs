using System;

namespace TesteNasaAPI.Models
{
    public class NASAImage
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string MediaType { get; set; }
        public DateTime Date { get; set; }
    }
}
