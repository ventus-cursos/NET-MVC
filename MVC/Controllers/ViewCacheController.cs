using System.Web.Mvc;

namespace Ventus.Controllers
{
    public class ViewCacheController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}