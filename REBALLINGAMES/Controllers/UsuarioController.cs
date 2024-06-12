using CapaDatos;
using CapaModelo;
using Microsoft.AspNetCore.Mvc;

namespace REBALLINGAMES.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult LoginRegistro()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                bool loginExitoso = CD_Usuario.Instancia.ValidarUsuario(usuario);
                if (loginExitoso)
                {
                    HttpContext.Session.SetString("Usuario", usuario.Nombre_Usuario);
                    return Json(new { resultado = true });
                }
                else
                {
                    return Json(new { resultado = false, mensaje = "Nombre de usuario o contraseña incorrectos." });
                }
            }
            return Json(new { resultado = false, mensaje = "Datos inválidos." });
        }

        [HttpPost]
        public IActionResult Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                bool registroExitoso = CD_Usuario.Instancia.RegistrarUsuario(usuario);
                if (registroExitoso)
                {
                    return Json(new { resultado = true });
                }
                else
                {
                    return Json(new { resultado = false, mensaje = "Error al registrar el usuario." });
                }
            }
            return Json(new { resultado = false, mensaje = "Datos inválidos." });
        }
    }
}
