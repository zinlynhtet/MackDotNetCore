using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackDotNetCore.ConsoleApp.Model
{
    [Table("tbl_blog")]
    public class BlogDataModel
    {
        [Key]
        [Column("blog_id")]
        public int? blog_id { get; set; }
        public string? blog_title { get; set; }
        public string? blog_authour { get; set; }
        public string? blog_content { get; set; }
    }
}
