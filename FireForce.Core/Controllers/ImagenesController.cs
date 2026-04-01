using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vista.Services;

namespace Vista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesController : ControllerBase
    {
        private readonly IImagenService _imagenService;

        public ImagenesController(IImagenService imagenService)
        {
            _imagenService = imagenService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerImagen(int id)
        {
            var imagen = await _imagenService.ObtenerImagenAsync(id);
            if (imagen == null)
                return NotFound();

            return File(imagen.Datos, imagen.Formato);
        }
    }
}
