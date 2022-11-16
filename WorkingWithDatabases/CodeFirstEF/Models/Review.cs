using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirstFilmReviewsDB.Models
{
    internal class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int FilmId { get; set; }
        public int ReviewerId { get; set; }
        public string? Commentary { get; set; }
        [Range(1, 10)]
        public int Rating { get; set; }
        public Film Film { get; set; }
    }
}
