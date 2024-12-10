using FacturasApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly FacturasService _service;

        public FacturasController(FacturasService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();
            if (result == null || !result.Any()) // Verifica si la lista está vacía
                return NoContent(); // Devuelve 204 No Content si no hay datos
            return Ok(result); // Devuelve 200 OK si hay datos
        }

        [HttpGet("comprador/{rut}")]
        public IActionResult GetByComprador(double rut)
        {
            var result = _service.GetByComprador(rut);
            if (result == null || !result.Any()) // Verifica si la lista está vacía
                return NoContent(); // Devuelve 204 No Content si no hay datos
            return Ok(result); // Devuelve 200 OK si hay datos
        }

        [HttpGet("top-comprador")]
        public IActionResult GetTopComprador()
        {
            var result = _service.GetTopComprador();
            if (result == null) // Si es un solo objeto, verifica si es null
                return NoContent(); // Devuelve 204 No Content si no hay datos
            return Ok(result); // Devuelve 200 OK si hay datos
        }

        [HttpGet("compradores-totales")]
        public IActionResult GetCompradoresTotales()
        {
            var result = _service.GetCompradoresTotales();
            if (result == null || !result.Any()) // Verifica si la lista está vacía
                return NoContent(); // Devuelve 204 No Content si no hay datos
            return Ok(result); // Devuelve 200 OK si hay datos
        }

        [HttpGet("comunas")]
        public IActionResult GetByComuna()
        {
            var result = _service.GetByComuna();
            if (result == null || !result.Any()) // Verifica si la lista está vacía
                return NoContent(); // Devuelve 204 No Content si no hay datos
            return Ok(result); // Devuelve 200 OK si hay datos
        }

        [HttpGet("comunas/{comuna}")]
        public IActionResult GetFacturasByComuna(double comuna)
        {
            var result = _service.GetFacturasByComuna(comuna);
            if (result == null || !result.Any()) // Verifica si la lista está vacía
                return NoContent(); // Devuelve 204 No Content si no hay datos
            return Ok(result); // Devuelve 200 OK si hay datos
        }

    }
}
