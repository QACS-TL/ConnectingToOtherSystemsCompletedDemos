using System;
using System.Collections.Generic;

namespace EFMoviesDatabaseFirst.Models
{
    public partial class Genre
    {
        public Genre()
        {
            MovieGenres = new HashSet<MovieGenre>();
        }

        public int GenreId { get; set; }
        public string? GenreName { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
