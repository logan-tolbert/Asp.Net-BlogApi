using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApi.Models
{
    public class Article
    {
        [Key]
        [Column("id", TypeName = "integer")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("title", TypeName = "text")]
        public string Title { get; set; } = string.Empty!;

        [Required]
        [Column("content", TypeName = "text")]
        public string Content { get; set; } = string.Empty!;

        [Required]
        [Column("author", TypeName = "text")]
        public string Author { get; set; } = string.Empty!;

        [Column("tags", TypeName = "text")]
        public string Tags { get; set; } = string.Empty!;

        [Column("created_at", TypeName = "text")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at", TypeName = "text")]
        public DateTime UpdatedAt { get; set; }

        public Article()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
