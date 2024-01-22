using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public decimal Add(decimal a, decimal b)
        {
            return a + b;
        }
        [HttpPost]
        public decimal Sub(decimal a, decimal b)
        {
            return a - b;
        }
        [HttpPut]
        public decimal Mult(decimal a, decimal b)
        {
            return a * b;
        }
        [HttpDelete]
        public decimal Div(decimal a, decimal b)
        {
            return a / b;
        }
    }
}
