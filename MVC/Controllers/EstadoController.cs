using System;
using System.Web.Mvc;

namespace Ventus.Controllers
{
    public class EstadoController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(DB.Estado.List(true));
        }

        [Authorize]
        [HttpGet]
        public ActionResult New()
        {
            return RedirectToAction("Edit", new { id = 0 });
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(DB.Estado.Get(id) ?? new BE.Estado());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(BE.Estado o)
        {
            DB.Estado.Save(o);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(string ids)
        {
            foreach (var id in Array.ConvertAll(ids.Split(','), Convert.ToInt32))
                DB.Estado.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
