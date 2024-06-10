using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador que enlista todos mis objectos del archivo json en base a mi modelo de datos y da una respuesta.
    /// Liga de la API para su consumo en frontend/postman: http://localhost:5293/api/data
    /// </summary>
    [ApiController]
    [Route("api/data")]
    public class DataController:ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Data>> Get()
        {
            try
            {
                var dataList = json_handler.ReadJson();
                return Ok(dataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, detail = ex.InnerException?.Message });
            }
            
        }
    }
}
