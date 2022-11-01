using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
using WebApplication1.Models.ViewModels.Pages;

namespace WebApplication1.Controllers
{
    public class PagesController : Controller
    {
        // GET: Index/{page}
        public ActionResult Index(string page = "")
        {
            if (page == "")
            {
                page = "home";
            }

            PageVM model;
            PagesDTO dto;

            using (var db = new Db())
            {
                if (!db.Pages.Any(x => x.Slug.Equals(page)))
                {
                    return RedirectToAction("Index", new { page = "" });
                }

                dto = db.Pages.Where(p => p.Slug == page).FirstOrDefault();
            }

            ViewBag.PageTitle = dto.Title;

            if (dto.HasSidebar == true)
            {
                ViewBag.Sidebar = "Yes";
            }
            else
            {
                ViewBag.Sidebar = "No";
            }

            model = new PageVM(dto);

            return View(model);
        }

        public ActionResult PagesMenuPartial()
        {
            List<PageVM> pageVMList;

            using (var db = new Db())
            {
                pageVMList = db.Pages.ToArray().OrderBy(p => p.Sorting).Where(p => p.Slug != "home")
                    .Select(p => new PageVM(p)).ToList();
            }

            return PartialView("_PagesMenuPartial", pageVMList);
        }

        public ActionResult SidebarPartial()
        {
            SidebarVM model;

            using (var db = new Db())
            {
                SidebarDTO dto = db.Sidebar.Find(3);
                model = new SidebarVM(dto);
            }

            return PartialView("_SidebarPartial", model);
        }
    }
}