using Microsoft.AspNetCore.Mvc;
using RestWithASPNet.Models;
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
            return Ok(_bookBusiness.FindAllBooks());
        }
    }
}
