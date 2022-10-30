using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
using WebApplication1.Models.ViewModels.Pages;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class SidebarController : Controller
    {
        // GET: Admin/Sidebar/Index
        public ActionResult Index()
        {
            List<SidebarVM> sidebarList;

            using (var db = new Db())
            {
                sidebarList = db.Sidebar.ToArray().Select(p => new SidebarVM(p)).ToList();
            }

            return View(sidebarList);
        }

        // GET: Admin/Sidebar/AddSidebar
        [HttpGet]
        public ActionResult AddSidebar()
        {
            return View();
        }

        // POST: Admin/Sidebar/AddSidebar
        [HttpPost]
        public ActionResult AddSidebar(SidebarVM sidebar)
        {

            if (!ModelState.IsValid)
            {
                return View(sidebar);
            }

            using (var db = new Db())
            {
                var dto = new SidebarDTO();

                dto.Body =  sidebar.Body;

                dto.Title = sidebar.Title.ToUpper();

                if (db.Sidebar.Where(p => p.Id != sidebar.Id).Any(p => p.Title == sidebar.Title))
                {
                    ModelState.AddModelError("", "That titile already exist.");
                    return View(sidebar);
                }

                db.Sidebar.Add(dto);
                db.SaveChanges();
            }

            TempData["SM"] = "Sidebar added!";

            return RedirectToAction("Index");
        }

        // GET: Admin/Sidebar/EditSidebar/id
        [HttpGet]
        public ActionResult EditSidebar(int id)
        {
            SidebarVM sidebar;

            using (var db = new Db())
            {
                if (db.Sidebar.FirstOrDefault(p => p.Id == id) is SidebarDTO dto)
                {
                     sidebar = new SidebarVM(dto);
                }
                else
                {
                    return Content("The sidebar does not exist");
                }
            }

            return View(sidebar);
        }

        // GET: Admin/Sidebar/EditSidebar/id
        [HttpPost]
        public ActionResult EditSidebar(SidebarVM sidebar)
        {

            if (!ModelState.IsValid)
            {
                return View(sidebar);
            }

            using (var db = new Db())
            {

                var dto = db.Sidebar.Find(sidebar.Id);

                dto.Title = sidebar.Title.ToUpper();

                if (db.Sidebar.Where(p => p.Id != sidebar.Id).Any(p => p.Title == sidebar.Title))
                {
                    ModelState.AddModelError("", "That titile already exist.");
                    return View(sidebar);
                }

                dto.Body = sidebar.Body;

                db.SaveChanges();
            }

            TempData["SM"] = "Sidebar edited!";

            return RedirectToAction("EditSidebar");
        }

        // GET: Admin/Sidebar/SidebarDetails/id
        [HttpGet]
        public ActionResult SidebarDetails(int id)
        {
            SidebarVM sidebar;

            using (var db = new Db())
            {
                if (db.Sidebar.FirstOrDefault(p => p.Id == id) is SidebarDTO dto)
                {
                     sidebar = new SidebarVM(dto);
                }
                else
                {
                    return Content("The sidebar does not exist");
                }
            }

            return View(sidebar);
        }

        // GET: Admin/Sidebar/DeleteSidebar/id
        public ActionResult DeleteSidebar(int id)
        {

            using (var db = new Db())
            {
                if (db.Sidebar.FirstOrDefault(p => p.Id == id) is SidebarDTO dto)
                {
                    db.Sidebar.Remove(dto);
                }
                else
                {
                    return Content("The sidebar does not exist");
                }

                db.SaveChanges();
            }

            TempData["SM"] = "Sidebar deleted!";

            return RedirectToAction("Index");
        }
    }
}