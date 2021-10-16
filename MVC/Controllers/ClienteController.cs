using System;
using System.Web.Mvc;

namespace Ventus.Controllers
{
    public class ClienteController : PagedController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(new Models.Cliente
            {
                PageIndex = PageIndex,
                MaxPage = MaxPage,
                Data = DB.Cliente.List(PageIndex * BL.Config.RowsPerPage, BL.Config.RowsPerPage)
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
            return View(DB.Cliente.Get(id) ?? new BE.Cliente());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(BE.Cliente o)
        {
            DB.Cliente.Save(o);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(string ids)
        {
            foreach (var id in Array.ConvertAll(ids.Split(','), Convert.ToInt32))
                DB.Cliente.Delete(id);
            return RedirectToAction("Index");
        }

        protected override int MaxPage
        {
            get { return DB.Cliente.Count() / BL.Config.RowsPerPage; }
        }

        public FileContentResult Foto(int id)
        {
            var cliente = DB.Cliente.Get(id);
            return cliente != null && cliente.Foto != null
                ? File(cliente.Foto, cliente.FotoImage.GetMimeType())
                : File(Resources.foto.ToByteArray(), Resources.foto.GetMimeType());
        }
    }
}
