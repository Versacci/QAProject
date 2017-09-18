using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QnAFitProject.Models
{
    public class Question
    {
        [Key]
        //primary key
        public int QuestionID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        //foregn key
        public int CategoryID { get; set; }

        //Navigation property
        public virtual Category Category { get; set; }
    }
}