using System;
using System.Web.Mvc;

namespace Ventus.Controllers
{
    public class OrdenController : PagedController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(new Models.Orden
            {
                PageIndex = PageIndex,
                MaxPage = MaxPage,
                Data = DB.Orden.List(PageIndex * BL.Config.RowsPerPage, BL.Config.RowsPerPage)
            });
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
            return View(DB.Orden.Get(id) ?? new BE.Orden());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(BE.Orden o)
        {
            if (ModelState.IsValid)
            {
                DB.Orden.Save(o);
                return RedirectToAction("Index");
            }
            return View(o);
        }

        [Authorize]
        public ActionResult Delete(string ids)
        {
            foreach (var id in Array.ConvertAll(ids.Split(','), Convert.ToInt32))
                DB.Orden.Delete(id);
            return RedirectToAction("Index");
        }

        protected override int MaxPage
        {
            get { return DB.Orden.Count() / BL.Config.RowsPerPage; }
        }
    }
}
