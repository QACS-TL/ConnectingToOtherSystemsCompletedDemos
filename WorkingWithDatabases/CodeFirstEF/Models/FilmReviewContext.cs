using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCodeFirstFilmReviewsDB.Models
{
    internal class FilmReviewContext : DbContext
    {
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local); Initial Catalog=FilmReviews; trusted_connection=true");
        }
    }
}
