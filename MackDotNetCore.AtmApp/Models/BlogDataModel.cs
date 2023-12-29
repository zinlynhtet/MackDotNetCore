using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MackDotNetCore.AtmApp.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        [Column("blog_id")]
        public int BlogId { get; set; }

        [Column("blog_cardnum")]
        public double CardNum { get; set; }

        [Column("blog_pin")]
        public int Pin { get; set; }

        [Column("blog_fname")]
        public string FirstName { get; set; }

        [Column("blog_lname")]
        public string LastName { get; set; }

        [Column("blog_balance")]
        public double Balance { get; set; }
    }

}
