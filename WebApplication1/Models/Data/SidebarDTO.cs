using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Data
{
    [Table("tblSidebar")]
    public class SidebarDTO
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Sorting { get; set; }

    }
}