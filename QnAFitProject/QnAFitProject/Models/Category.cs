using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QnAFitProject.Models
{
    public class Category
    {
        [Key]
        //primary key
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }

        //Navigation property
        //public virtual Question Question { get; set; }
    }
}