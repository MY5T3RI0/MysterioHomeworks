using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
using WebApplication1.Models.ViewModels.Homeworks;

namespace WebApplication1.Controllers
{
    public class HomeworksController : Controller
    {
        // GET: Homeworks
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Pages");
        }

        public ActionResult CategoryMenuPartial()
        {
            List<CategoryVM> categoryVMList;

            using (var db = new Db())
            {
                categoryVMList = db.Categories.ToArray().OrderBy(c => c.Sorting)
                    .Select(c => new CategoryVM(c)).ToList();
            }

            return PartialView("_CategoryMenuPartial", categoryVMList);
        }

        public ActionResult Category(string name)
        {
            List<WorkVM> workVMList;

            using (var db = new Db())
            {
                var categoryDTO = db.Categories.Where(c => c.Slug == name).FirstOrDefault();

                int catId = categoryDTO.Id;

                workVMList = db.Works.ToArray().Where(p => p.CategoryId == catId)
                    .OrderBy(p => p.Date)
                    .Select(p => new WorkVM(p)).ToList();
                var productCat = db.Works.Where(p => p.CategoryId == catId).FirstOrDefault();

                if (productCat == null)
                {
                    var catName = db.Categories.Where(c => c.Slug == name).Select(c => c.Name).FirstOrDefault();
                    ViewBag.CategoryName = catName;
                }
                else
                {
                    ViewBag.CategoryName = productCat.CategoryName;
                }
            }

            return View(workVMList);
        }

        [ActionName("work-details")]
        public ActionResult WorkDetails(int ? id)
        {
            WorkVM model;
            WorksDTO dto;

            using (var db = new Db())
            {
                if (!db.Works.Any(p => p.Id == id))
                {
                    return RedirectToAction("Index", "Homeworks");
                }

                dto = db.Works.Where(p => p.Id == id).FirstOrDefault();

                id = dto.Id;
            }
            model = new WorkVM(dto);
            model.GalleryImages = Directory
                .EnumerateFiles(Server.MapPath("~/Images/Uploads/Works/" + id + "/Gallery/Thumbs"))
                .Select(f => Path.GetFileName(f));

            return View("WorkDetails", model);

        }
    }
}