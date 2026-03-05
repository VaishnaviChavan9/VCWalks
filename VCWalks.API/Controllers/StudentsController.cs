using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VCWalks.API.Controllers
{
    //https://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET://https://localhost:portnumber/api/Students
        [HttpGet]
        public IActionResult getAllStudents()
        {
            string[] Students = { "Jane", "Jini", "Micky" };

            
            return Ok(Students);
        }
    }
}
