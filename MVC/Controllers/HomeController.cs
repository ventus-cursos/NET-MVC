using System.Web.Mvc;
using System.Web.Security;

namespace Ventus.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(BE.Usuario usuario)
        {
            if (!ModelState.IsValid) return View();

            //TODO: authenticate (username,password)
            if ("admin".Equals(usuario.Nombre) && !string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                FormsAuthentication.SetAuthCookie(usuario.Nombre, usuario.Recuerdame);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "El Nombre de usuario o Contraseña es inválido.");
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
