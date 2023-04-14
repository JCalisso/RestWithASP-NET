using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNet.Models
{
    // Anotation - usado para especificar um nome referente  
    // ao nome usado na base quando os mesmos são diferentes
    [Table("books")]
    public class Book
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}
