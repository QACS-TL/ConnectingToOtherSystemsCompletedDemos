using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NoSQLFilmReviews.Models
{
    internal class Review
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public string? Commentary { get; set; }
        public int Rating { get; set; }
    }
}
