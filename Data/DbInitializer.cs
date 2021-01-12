using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaModel.Models;

namespace CinemaModel.Data
{
    public class DbInitializer
    {
        public static void Initialize(CinemaContext context)
        {
            context.Database.EnsureCreated();
            if (context.Categories.Any())
            {
                return; // BD a fost creata anterior
            }

            var categories = new Category[]
            {
                new Category{MovieGenre="Comedie"},
                new Category{MovieGenre="Actiune"},
                new Category{MovieGenre="Drama"},
                new Category{MovieGenre="Aventura"},
                new Category{MovieGenre="Western"},
                new Category{MovieGenre="Razboi"},
                new Category{MovieGenre="Animatie"},
            };
            foreach (Category s in categories)
            {
                context.Categories.Add(s);
            }
            context.SaveChanges();

            var studios = new Studio[]
            {
                new Studio{StudioID=1, StudioName="20th Century Fox", Founded=DateTime.Parse("1935-05-31"), ParentOrganization="Walt Disney Studios"},
                new Studio{StudioID=2, StudioName="Paramount Pictures", Founded=DateTime.Parse("1912-05-08"), ParentOrganization="ViacomCBS"},
                new Studio{StudioID=3, StudioName="Warner Bros", Founded=DateTime.Parse("1923-04-04"), ParentOrganization="WarnerMedia"},
                new Studio{StudioID=4, StudioName="Universal Pictures", Founded=DateTime.Parse("1912-04-30"), ParentOrganization="NBCUniversal"},
                new Studio{StudioID=5, StudioName="Walt Disney Pictures", Founded=DateTime.Parse("1923-10-16"), ParentOrganization="Walt Disney Studios"},
                new Studio{StudioID=6, StudioName="Columbia Pictures", Founded=DateTime.Parse("1924-01-10"), ParentOrganization="Sony Pictures"},
                new Studio{StudioID=7, StudioName="United Artists", Founded=DateTime.Parse("1919-02-05"), ParentOrganization="Metro-Goldwyn-Mayer"},
            };
            foreach (Studio c in studios)
            {
                context.Studios.Add(c);
            }
            context.SaveChanges();

            var movies = new Movie[]
            {
                new Movie{Title="The Good, the Bad and the Ugly", CategoryID=5, StudioID=7, FilmDirector="Sergio Leone",ReleaseDate=DateTime.Parse("1966-12-23"),BoxOffice="25.1 milioane USD"},
                new Movie{Title="Saving Private Ryan" ,CategoryID=6, StudioID=2, FilmDirector="Steven Spielberg",ReleaseDate=DateTime.Parse("1998-11-06"),BoxOffice="482.3 milioane USD"},
                new Movie{Title="Titanic" ,CategoryID=3, StudioID=1, FilmDirector="James Cameron",ReleaseDate=DateTime.Parse("1997-11-18"),BoxOffice="2.195 miliarde USD"},
            };
            foreach (Movie e in movies)
            {
                context.Movies.Add(e);
            }
            context.SaveChanges();

            var actors = new Actor[]
            {
            new Actor{Name="Clint Eastwood", BirthDate=DateTime.Parse("1930-05-31"), BirthPlace="San Francisco, California, United States"},
            new Actor{Name="Tom Hanks", BirthDate=DateTime.Parse("1956-07-09"), BirthPlace="Concord, California, United States"},
            new Actor{Name="Leonardo DiCaprio", BirthDate=DateTime.Parse("1974-11-11"), BirthPlace="Los Angeles, California, United States"},
            };
            foreach (Actor a in actors)
            {
                context.Actors.Add(a);
            }
            context.SaveChanges();

            var appearancemovies = new AppearanceMovie[]
            {
                new AppearanceMovie {MovieID = movies.Single(c => c.Title == "The Good, the Bad and the Ugly" ).MovieID, ActorID = actors.Single(i => i.Name =="Clint Eastwood").ActorID},
                new AppearanceMovie {MovieID = movies.Single(c => c.Title == "Saving Private Ryan" ).MovieID, ActorID = actors.Single(i => i.Name =="Tom Hanks").ActorID},
                new AppearanceMovie {MovieID = movies.Single(c => c.Title == "Titanic" ).MovieID, ActorID = actors.Single(i => i.Name =="Leonardo DiCaprio").ActorID},
            };
            foreach (AppearanceMovie am in appearancemovies)
            {
                context.AppearanceMovies.Add(am);
            }
            context.SaveChanges();

        }
    }
}
