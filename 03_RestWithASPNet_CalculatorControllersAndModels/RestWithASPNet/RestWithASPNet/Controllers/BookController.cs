using Microsoft.AspNetCore.Mvc;
using RestWithASPNet.Data.VO;
using RestWithASPNet.Business;

namespace RestWithASPNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]  // -- prefixo API para todos os endpoints 
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, 
                              IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        //FindAll
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) { return NotFound(); }

            return Ok(book);
        }

        [HttpPut]
        public IActionResult Put([FromBody] BookVO book)  // Recebe um payload
        {
            if (book == null) return BadRequest();

            return Ok(_bookBusiness.Update(book));
        }


        [HttpPost]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();

            return Ok(_bookBusiness.Create(book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);

            return NoContent();
        }
    }
}
