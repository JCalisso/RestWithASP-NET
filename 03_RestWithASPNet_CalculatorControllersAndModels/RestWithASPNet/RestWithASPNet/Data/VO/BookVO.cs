using RestWithASPNet.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNet.Models
{
    public class BookVO
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public DateTime LaunchDate { get; set; }

        public decimal Price { get; set; }
    }
}
