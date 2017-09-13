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
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}