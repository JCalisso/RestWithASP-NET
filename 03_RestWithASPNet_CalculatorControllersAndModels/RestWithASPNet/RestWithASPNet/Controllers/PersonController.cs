using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        
        // sum
        [HttpGet("")] //path espec�fico para o m�todo get
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            return BadRequest("Invalid Input");
        }

       
    }
}