using System;

namespace NewsApp.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public NewsCategory Category { get; set; }
    }
}