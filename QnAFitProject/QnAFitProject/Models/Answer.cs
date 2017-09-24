using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QnAFitProject.Models
{
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }

        [Required]
        public string Text { get; set; }

        public string TempString { get; set; }

        //foreign key
        public int QuestionID { get; set; }

        //Navigation property
        public virtual Question Question { get; set; }
    }
}