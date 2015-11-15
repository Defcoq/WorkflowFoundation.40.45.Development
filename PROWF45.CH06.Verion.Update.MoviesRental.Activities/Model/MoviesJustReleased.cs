using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWF45.CH06.Verion.Update.MoviesRental.Activities.Model
{
    public class MoviesJustReleased : CodeActivity<List<Movie>>
    {
        [RequiredArgument]
        public InArgument<Movie> inBasedOnMovie { get; set; }
        protected override List<Movie> Execute(CodeActivityContext context)
        {
            List<Movie> retMovies = null;
            var basedOnMovie = inBasedOnMovie.Get(context);
            if (basedOnMovie.Rating == "PG")
            {//could do some logic here to return only movies based on a movie's rating
                retMovies = new List<Movie> 
                {
                    new Movie
                    { 
                        Rating="PG", 
                        MovieName="The Walking Dead (Seasons 1 and 2)", 
                        Price=Convert.ToDecimal(4.50)
                    },
                    new Movie
                    {
                        Rating="PG", 
                        MovieName="Promethius", 
                        Price=Convert.ToDecimal(5.50)
                    },
                    new Movie
                    {
                        Rating="PG", 
                        MovieName="Hotel Transylvania", 
                        Price=Convert.ToDecimal(4.50)
                    }
                };
            }
            return retMovies;
        }
    }
}
