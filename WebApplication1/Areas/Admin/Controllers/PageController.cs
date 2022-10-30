using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
using WebApplication1.Models.ViewModels.Pages;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class PageController : Controller
    {
        // GET: Admin/Page/Index
        [HttpGet]
        public ActionResult Index()
        {
            List<PageVM> pageList;

            using (var db = new Db())
            {
                pageList = db.Pages.ToArray().OrderBy(p => p.Sorting).Select(p => new PageVM(p)).ToList();
            }

            return View(pageList);
        }

        // GET: Admin/Page/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }

        // POST: Admin/Page/AddPage
        [HttpPost]
        public ActionResult AddPage(PageVM page)
        {

            if (!ModelState.IsValid)
            {
                return View(page);
            }

            using (var db = new Db())
            {
                string slug;

                var dto = new PagesDTO();

                dto.Title = page.Title.ToUpper();

                if (!string.IsNullOrWhiteSpace(page.Slug))
                {
                    slug = page.Slug.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = page.Title.Replace(" ", "-").ToLower();
                }

                if (db.Pages.Where(p => p.Id != page.Id).Any(p => p.Title == page.Title))
                {
                    ModelState.AddModelError("", "That titile already exist.");
                    return View(page);
                }
                else if (db.Pages.Where(p => p.Id != page.Id).Any(p => p.Slug == slug))
                {
                    ModelState.AddModelError("", "That slug already exist.");
                    return View(page);
                }

                dto.Slug = slug;

                dto.Body = page.Body;

                dto.HasSidebar = page.HasSidebar;

                dto.Sorting = 100;

                db.Pages.Add(dto);
                db.SaveChanges();
            }

            TempData["SM"] = "Page added!";

            return RedirectToAction("Index");
        }

        // GET: Admin/Page/EditPage/id
        [HttpGet]
        public ActionResult EditPage(int id)
        {
            PageVM page;

            using (var db = new Db())
            {
                if(db.Pages.FirstOrDefault(p => p.Id == id) is PagesDTO dto)
                {
                    page = new PageVM(dto);
                }
                else
                {
                    return Content("The page does not exist");
                }
            }

            return View(page);
        }

        // GET: Admin/Page/EditPage/id
        [HttpPost]
        public ActionResult EditPage(PageVM page)
        {

            if (!ModelState.IsValid)
            {
                return View(page);
            }

            using (var db = new Db())
            {
                string slug = "home";

                var dto = db.Pages.Find(page.Id);

                dto.Title = page.Title.ToUpper();

                if (page.Slug != "home")
                {
                    if (!string.IsNullOrWhiteSpace(page.Slug))
                    {
                        slug = page.Slug.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = page.Title.Replace(" ", "-").ToLower();
                    } 
                }

                if (db.Pages.Where(p => p.Id != page.Id).Any(p => p.Title == page.Title))
                {
                    ModelState.AddModelError("", "That titile already exist.");
                    return View(page);
                }
                else if (db.Pages.Where(p => p.Id != page.Id).Any(p => p.Slug == slug))
                {
                    ModelState.AddModelError("", "That slug already exist.");
                    return View(page);
                }

                dto.Slug = slug; 

                dto.Body = page.Body;

                dto.HasSidebar = page.HasSidebar;

                db.SaveChanges();
            }

            TempData["SM"] = "Page edited!";

            return RedirectToAction("EditPage");
        }

        // GET: Admin/Page/PageDetails/id
        [HttpGet]
        public ActionResult PageDetails(int id)
        {
            PageVM page;

            using (var db = new Db())
            {
                if (db.Pages.FirstOrDefault(p => p.Id == id) is PagesDTO dto)
                {
                    page = new PageVM(dto);
                }
                else
                {
                    return Content("The page does not exist");
                }
            }

            return View(page);
        }

        // GET: Admin/Page/DeletePage/id
        public ActionResult DeletePage(int id)
        {

            using (var db = new Db())
            {
                if (db.Pages.FirstOrDefault(p => p.Id == id) is PagesDTO dto)
                {
                    db.Pages.Remove(dto);
                }
                else
                {
                    return Content("The page does not exist");
                }

                db.SaveChanges();
            }

            TempData["SM"] = "Page deleted!";

            return RedirectToAction("Index");
        }

        // GET: Admin/Page/ReorderPages/id
        [HttpPost]
        public void ReorderPages(int[] id)
        {
            using (var db = new Db())
            {
                int count = 1;
                PagesDTO dto;
                foreach (var pageId in id)
                {
                    dto = db.Pages.Find(pageId);
                    dto.Sorting = count;
                    db.SaveChanges();
                    count++;
                }

            }
        }
    }
}