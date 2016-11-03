using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVH7_Cinema.Domain.Entities;

namespace IVH7_Cinema.WebUI.Website.Models
{
    public class MoviesAddViewModel
    {
        public Movie Movie{get; set;}

        public List<Genre> Genres{get; set;}

        public List<Language> Languages{get; set;}

        public List<Rating> Ratings{get; set;}

        public List<Cinema> Cinemas{get; set;}
    }
}