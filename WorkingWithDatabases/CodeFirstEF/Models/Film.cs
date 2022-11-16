using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirstFilmReviewsDB.Models
{
    internal class Film
    {
        [Key]
        public int FilmId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required, MaxLength(100)]
        public string Overview { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public long? Revenue { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
