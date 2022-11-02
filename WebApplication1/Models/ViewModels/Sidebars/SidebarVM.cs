using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;

namespace WebApplication1.Models.ViewModels.Pages
{
    public class SidebarVM
    {
        public SidebarVM()
        {

        }
        public SidebarVM(SidebarDTO row)
        {
            Id = row.Id;
            Title = row.Title;
            Body = row.Body;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 3)]
        [AllowHtml]
        public string Body { get; set; }
        public int Sorting { get; set; }
    }
}