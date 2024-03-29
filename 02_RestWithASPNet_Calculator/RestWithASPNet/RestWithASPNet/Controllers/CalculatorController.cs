using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        // ex da URL usada
        // "https://localhost:7266/Calculator/sum/1/2"
        // a na url temos o local host (caminho da api)
        // o controller Calculator *n necessita definir como CalculatorController
        // m�todo get sum e os dois par�metros de entrada recebidos via PATH

        
        // sum
        [HttpGet("sum/{firstNumber}/{secondNumber}")] //path espec�fico para o m�todo get
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            //Valida se o valor � num�rico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid Input");
        }

       
        //sub
        [HttpGet("subtraction/{firstNumber}/{secondNumber}")] //path espec�fico para o m�todo get
        public IActionResult Subtraction(string firstNumber, string secondNumber)
        {
            //Valida se o valor � num�rico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(sub.ToString());
            }

            return BadRequest("Invalid Input");
        }
        

        // mult
        [HttpGet("multiplication/{firstNumber}/{secondNumber}")] //path espec�fico para o m�todo get
        public IActionResult Multiplication(string firstNumber, string secondNumber)
        {
            //Valida se o valor � num�rico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var mult = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(mult.ToString());
            }

            return BadRequest("Invalid Input");
        }


        // div
        [HttpGet("div/{firstNumber}/{secondNumber}")] //path espec�fico para o m�todo get
        public IActionResult Div(string firstNumber, string secondNumber)
        {
            //Valida se o valor � num�rico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var div = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(div.ToString());
            }

            return BadRequest("Invalid Input");
        }

        // mean
        [HttpGet("mean/{firstNumber}/{secondNumber}")] //path espec�fico para o m�todo get
        public IActionResult Mean(string firstNumber, string secondNumber)
        {
            //Valida se o valor � num�rico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var mod = ((ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber))) / 2;
                return Ok(mod.ToString());
            }

            return BadRequest("Invalid Input");
        }

        // raiz
        [HttpGet("square-root/{number}")] //path espec�fico para o m�todo get
        public IActionResult SquareRoot(string number)
        {
            //Valida se o valor � num�rico
            if (IsNumeric(number))
            {
                var raiz = Math.Sqrt((double)ConvertToDecimal(number));
                return Ok(raiz.ToString());
            }

            return BadRequest("Invalid Input");
        }

        //m�todo que converte o valor inserido para decimal
        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;

            if(decimal.TryParse(strNumber, out decimalValue)) // se o tryparse for verdeiro (tentar converter o valor e funcionar) ele retorna o valor convertido 
            {
                return decimalValue;
            }

            return 0;
        }


        //m�todo que valida se o valor inserido � num�rico
        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(strNumber, 
                                            System.Globalization.NumberStyles.Any, 
                                            System.Globalization.NumberFormatInfo.InvariantInfo, 
                                            out number); 

            return isNumber;
        }
    }
}