using CapaDatos;
using CapaModelo;
using Microsoft.AspNetCore.Mvc;

namespace REBALLINGAMES.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult CrearP()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Obtener()
        {
            List<Producto> lista = CD_Producto.Instancia.ObtenerProducto();
            return Json(new { data = lista });

        }

        [HttpPost]
        public JsonResult Guardar(Producto objeto)
        {
            bool respuesta = false;
            if (objeto.id_Producto == 0)
            {
                respuesta = CD_Producto.Instancia.RegistrarProducto(objeto);

            }
            else
            {
                respuesta = CD_Producto.Instancia.ModificarProducto(objeto);
            }
            return Json(new { resultado = respuesta });
        }


        public JsonResult GuardarCat(Categoria objeto)
        {
            bool respuesta = false;
            if (objeto.id_Categoria == 0)
            {
                respuesta = CD_Categoria.Instancia.RegistrarCategoria(objeto);

            }
            else
            {
                respuesta = CD_Categoria.Instancia.ModificarCategoria(objeto);
            }
            return Json(new { resultado = respuesta });
        }


        [HttpDelete]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CD_Producto.Instancia.EliminarProducto(id);
            return Json(new { resultado = respuesta });
        }


    }
}
