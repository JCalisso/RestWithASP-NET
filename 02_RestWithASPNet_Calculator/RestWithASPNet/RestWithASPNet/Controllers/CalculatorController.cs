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
        // método get sum e os dois parâmetros de entrada recebidos via PATH

        /*
        // sum
        [HttpGet("sum/{firstNumber}/{secondNumber}")] //path específico para o método get
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            //Valida se o valor é numérico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid Input");
        }
        */

        /*
        //sub
        [HttpGet("sub/{firstNumber}/{secondNumber}")] //path específico para o método get
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            //Valida se o valor é numérico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(sub.ToString());
            }

            return BadRequest("Invalid Input");
        }
        */

        /*
        // mult
        [HttpGet("mult/{firstNumber}/{secondNumber}")] //path específico para o método get
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            //Valida se o valor é numérico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var mult = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(mult.ToString());
            }

            return BadRequest("Invalid Input");
        }
        */


        /*
        // div
        [HttpGet("div/{firstNumber}/{secondNumber}")] //path específico para o método get
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            //Valida se o valor é numérico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var div = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(div.ToString());
            }

            return BadRequest("Invalid Input");
        }
        */

        /*
        // mod
        [HttpGet("mod/{firstNumber}/{secondNumber}")] //path específico para o método get
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            //Valida se o valor é numérico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var mod = ((ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
                return Ok(mod.ToString());
            }

            return BadRequest("Invalid Input");
        }
         */

        /*
        // raiz
        [HttpGet("raiz/{number}")] //path específico para o método get
        public IActionResult Get(string number)
        {
            //Valida se o valor é numérico
            if (IsNumeric(number))
            {
                var raiz = Math.Sqrt(ConvertToDouble(number));
                return Ok(raiz.ToString());
            }

            return BadRequest("Invalid Input");
        }
        */

        //raiz

        //método que converte o valor inserido para decimal
        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;

            if(decimal.TryParse(strNumber, out decimalValue)) // se o tryparse for verdeiro (tentar converter o valor e funcionar) ele retorna o valor convertido 
            {
                return decimalValue;
            }

            return 0;
        }

        private double ConvertToDouble(string strNumber)
        {
            double doubleValue;

            if (double.TryParse(strNumber, out doubleValue)) // se o tryparse for verdeiro (tentar converter o valor e funcionar) ele retorna o valor convertido 
            {
                return doubleValue;
            }

            return 0;
        }


        //método que valida se o valor inserido é numérico
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