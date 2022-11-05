using System.ComponentModel.DataAnnotations;

namespace SampleCodeFirst.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Content { get; set; }

        public int Read { get; set; }

        public Guid UserId { get; set; }

        public DateTime PostTime { get; set; }

        public virtual User User { get; set; }
    }
}