using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;

namespace WebApplication1.Models.ViewModels.Homeworks
{
    public class WorkVM
    {
        public WorkVM()
        {

        }

        public WorkVM(WorksDTO row)
        {
            Id = row.Id;
            Slug = row.Slug;
            Name = row.Name;
            Description = row.Description;
            Date = row.Date;
            CategoryName = row.CategoryName;
            CategoryId = row.CategoryId;
            ImageName = row.ImageName;
        }

        public int Id { get; set; }
        public string Slug { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string CategoryName { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [DisplayName("Name")]
        public string ImageName { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<string> GalleryImages { get; set; }
    }
}