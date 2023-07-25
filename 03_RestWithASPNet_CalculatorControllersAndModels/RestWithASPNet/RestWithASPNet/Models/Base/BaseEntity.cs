using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNet.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
