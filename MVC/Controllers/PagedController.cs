using System.Web.Mvc;

namespace Ventus.Controllers
{
    public abstract class PagedController : Controller
    {
        protected int PageIndex
        {
            get { return Session[ControllerContext.Controller + "PageIndex"] as int? ?? 0; }
            set { Session[ControllerContext.Controller + "PageIndex"] = value; }
        }

        protected abstract int MaxPage { get; }

        [Authorize]
        public ActionResult NextPage()
        {
            if (PageIndex < MaxPage) PageIndex++;
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult PrevPage()
        {
            if (PageIndex > 0) PageIndex--;
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult GoPage(int id)
        {
            PageIndex = id;
            return RedirectToAction("Index");
        }
    }
}
