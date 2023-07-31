using RestWithASPNet.Hypermedia;
using RestWithASPNet.Hypermedia.Abstract;

namespace RestWithASPNet.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        internal object id;

        public int Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public DateTime LaunchDate { get; set; }

        public decimal Price { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
