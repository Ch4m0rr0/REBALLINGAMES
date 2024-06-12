using CapaDatos;
using CapaModelo;
using Microsoft.AspNetCore.Mvc;

namespace REBALLINGAMES.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Obtener()
        {
            List<Empleado> listaEmpleado = CD_Empleado.Instancia.ObtenerEmpleado();
            return Json(new { data = listaEmpleado });
        }

        [HttpPost]
        public JsonResult Guardar(Empleado Empleado)
        {
            bool respuesta = false;
            if (Empleado.id_Empleado == 0)
            {
                respuesta = CD_Empleado.Instancia.RegistrarEmpleado(Empleado);
            }
            else
            {
                respuesta = CD_Empleado.Instancia.ModificarEmpleado(Empleado);
            }
            return Json(new { resultado = respuesta });
        }

        [HttpDelete]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Empleado.Instancia.EliminarEmpleado(id);
            return Json(new { resultado = respuesta });
        }
    }
}

