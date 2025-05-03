using RestWithASPNet.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNet.Models
{
    // Anotation - usado para especificar um nome referente  
    // ao nome usado na base quando os mesmos são diferentes
    [Table("person")]
    public class Person : BaseEntity
    {

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("enabled")]
        public bool Enabled { get; set; }
    }
}
