using CapaDatos;
using CapaModelo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace REBALLINGAMES.Controllers
{
    public class AlmacenController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Obtener()
        {
            List<Almacen> listaAlmacenes = CD_Almacen.Instancia.ObtenerAlmacenes();
            return Json(new { data = listaAlmacenes });
        }

        [HttpPost]
        public JsonResult Guardar(Almacen almacen)
        {
            bool respuesta = false;
            if (almacen.id_Almacen == 0)
            {
                respuesta = CD_Almacen.Instancia.RegistrarAlmacen(almacen);
            }
            else
            {
                respuesta = CD_Almacen.Instancia.ModificarAlmacen(almacen);
            }
            return Json(new { resultado = respuesta });
        }

        [HttpDelete]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Almacen.Instancia.EliminarAlmacen(id);
            return Json(new { resultado = respuesta });
        }
    }
}
