using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QnAFitProject.Models
{
    public class FitnessDbContext : ApplicationDbContext
    {
        public FitnessDbContext()
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Question> Question { get; set; }
    }
}