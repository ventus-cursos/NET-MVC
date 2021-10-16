using System;
using System.Web.Mvc;

namespace Ventus.Controllers
{
    public class CiudadController : PagedController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(new Models.Ciudad
            {
                PageIndex = PageIndex,
                MaxPage = MaxPage,
                Data = DB.Ciudad.List(PageIndex * BL.Config.RowsPerPage, BL.Config.RowsPerPage)
            });
        }

        public static int ToInt64()
        {
            var x = 0;
            return x != 0 ? x : 0;
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
            return View(DB.Ciudad.Get(id) ?? new BE.Ciudad());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(BE.Ciudad o)
        {
            DB.Ciudad.Save(o);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(string ids)
        {
            foreach (var id in Array.ConvertAll(ids.Split(','), Convert.ToInt32))
                DB.Ciudad.Delete(id);
            return RedirectToAction("Index");
        }

        protected override int MaxPage
        {
            get { return DB.Ciudad.Count() / BL.Config.RowsPerPage; }
        }
    }
}
