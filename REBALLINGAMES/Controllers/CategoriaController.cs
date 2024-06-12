using CapaDatos;
using CapaModelo;
using Microsoft.AspNetCore.Mvc;

namespace REBALLINGAMES.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Obtener()
        {
            List<Categoria> listaCategorias = CD_Categoria.Instancia.ObtenerCategoria();
            return Json(new { data = listaCategorias });
        }

        [HttpPost]
        public JsonResult Guardar(Categoria categoria)
        {
            bool respuesta = false;
            if (categoria.id_Categoria == 0)
            {
                respuesta = CD_Categoria.Instancia.RegistrarCategoria(categoria);
            }
            else
            {
                respuesta = CD_Categoria.Instancia.ModificarCategoria(categoria);
            }
            return Json(new { resultado = respuesta });
        }

        [HttpDelete]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Categoria.Instancia.EliminarCategoria(id);
            return Json(new { resultado = respuesta });
        }
    }
}
