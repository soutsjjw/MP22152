using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleMinimal.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string UserName { get; set; } = default!;

        public virtual ICollection<Article> Articles { get; set; } = default!;
    }
}
