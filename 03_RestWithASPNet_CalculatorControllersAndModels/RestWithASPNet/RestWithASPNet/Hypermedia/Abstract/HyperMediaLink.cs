using System.Text;

namespace RestWithASPNet.Hypermedia.Abstract
{
    public class HyperMediaLink
    {
        public string Rel { get; set; }

        private string href; // href temporário para tratativa
        public string Href {
            get 
            {
                object _lock = new object(); // lockado por ser chamado em condição de paralelismo
                lock (_lock) 
                {
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").ToString(); 
                }
            } 
            set { 
                href = value;  
            } 
        } // quando existe uma / (Barra), o .NET converte para "%2f" ex: localhost:8080%2fapi. O link mas o ideal é ser tratado
        
        public string Type { get; set; }
        public string Action { get; set; }
    }
}
