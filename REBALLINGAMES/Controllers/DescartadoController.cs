using CapaDatos;
using CapaModelo;
using Microsoft.AspNetCore.Mvc;

namespace REBALLINGAMES.Controllers
{
    public class DescartadoController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Obtener()
        {
            List<Descartado> listaDescartado = CD_Descartado.Instancia.ObtenerDescartados();
            return Json(new { data = listaDescartado });
        }

        [HttpPost]
        public JsonResult Guardar(Descartado Descartado)
        {
            bool respuesta = false;
            if (Descartado.id_Descartado == 0)
            {
                respuesta = CD_Descartado.Instancia.RegistrarDescartado(Descartado);
            }
            else
            {
                respuesta = CD_Descartado.Instancia.ModificarDescartado(Descartado);
            }
            return Json(new { resultado = respuesta });
        }

        [HttpDelete]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Descartado.Instancia.EliminarDescartado(id);
            return Json(new { resultado = respuesta });
        }
    }
}
    

