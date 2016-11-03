using IVH7_Cinema.Domain.Abstract;
using IVH7_Cinema.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace IVH7_Cinema.Domain.Concrete
{
    [ExcludeFromCodeCoverage]
    public class EFCinemaRepository : ICinemaRepository
    {
        private EFDbContext Context = new EFDbContext();

        public EFCinemaRepository(EFDbContext Context)
        {
            this.Context = Context;
        }

        public IEnumerable<Movie> Movies
        {
            get { return Context.Movies.Include(x => x.Genres).ToList(); }
        }

        public IEnumerable<Questionnaire> Questionnaires
        {
            get { return Context.Questionnaires; }
        }

        public void AddQuestionnaire(Questionnaire Q) {
            Context.Questionnaires.Add(Q);
            Context.SaveChanges();

        }

        public IEnumerable<MovieReview> Reviews
        {
            get { return Context.Reviews; }
        }

        public void AddMovieReview(MovieReview R)
        {
            Context.Reviews.Add(R);
            Context.SaveChanges();
        }
        //Get functionality
        public Movie GetMovie(Int64 ID)
        {
            return Context.Movies.Where(x => x.MovieID == ID).Include(x => x.Ratings).FirstOrDefault();
            //List<Movie> movies = Movies.ToList<Movie>();
            //foreach (Movie m in movies)
            //{
            //    if (m.Title.Equals(title))
            //    {
            //        return m;
            //    }
            //}
            //return null;
        }

        //Get functionality
        public Order GetOrder(Int64 ID)
        {
            return Context.Orders.Where(x => x.OrderID == ID).Include(x => x.Tickets).FirstOrDefault();
        }

        public void ChangeOrderStatus(Int64 OrderID, string Status)
        {
            Context.Orders.Where(x => x.OrderID == OrderID).First().Status = Status;
            Context.SaveChanges();
        }

        public void DeleteOrder(Int64 OrderID)
        {
            Context.Orders.Remove(Orders.Where(x => x.OrderID == OrderID).First());
            Context.SaveChanges();
        }

        public void ChangeLostObject(LostObject LostObject)
        {
            Context.Entry(LostObject).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public Screen GetScreen(Int64 Number)
        {
            return Context.Screens.Where(x => x.ScreenID == Number).Include(x => x.Seats).FirstOrDefault();
            //List<Screen> screens = Screens.ToList<Screen>();
            //foreach (Screen m in screens)
            //{
            //    if (m.Number.Equals(number))
            //    {
            //        return m;
            //    }
            //}
            //return null;
        }

        //Add functionality
        public void AddMovies(Movie Movie)
        {
            Context.Movies.Add(Movie);
            Context.SaveChanges();
        }

        public void AddLanguage(Language Lang)
        {
            Context.Languages.Add(Lang);
            Context.SaveChanges();
        }

        public IEnumerable<Cinema> Cinemas
        {
            get { return Context.Cinemas; }
        }

        //Get functionality
        public Cinema GetCinema(string Name)
        {
            return Context.Cinemas.Where(x => x.Name == Name).FirstOrDefault();
        }

        public IEnumerable<Language> Languages
        {
            get { return Context.Languages; }
        }

        public IEnumerable<Genre> Genres
        {
            get { return Context.Genres; }
        }

        public IEnumerable<LostObject> LostObjects
        {
            get { return Context.LostObjects; }
        }

        public IEnumerable<Rating> Ratings
        {
            get { return Context.Ratings; }
        }

        public IEnumerable<Subscriber> Subscribers
        {
            get { return Context.Subscribers; }
        }

        //Add functionality
        public void AddCinemas(Cinema Cinema)
        {
            Context.Cinemas.Add(Cinema);
            Context.SaveChanges();
        }

        public void AddSubscriber(Subscriber Subscriber)
        {
            Context.Subscribers.Add(Subscriber);
            Context.SaveChanges();
        }

        public void DeleteSubscriber(Subscriber Subscriber)
        {
            Context.Subscribers.Remove(Subscriber);
            Context.SaveChanges();
        }

        public void AddLostObject(LostObject Lostobject)
        {
            Context.LostObjects.Add(Lostobject);
            Context.SaveChanges();
        }

        public void AddGenre(Genre Genre)
        {
            Context.Genres.Add(Genre);
            Context.SaveChanges();
        }

        public IEnumerable<Screen> Screens
        {
            get
            {
                //IEnumerable<Seat> seats = Context.Seats;
                //List<Seat> seatsList = seats.ToList();

                //foreach (Screen s in Context.Screens)
                //{
                //    s.Seats = seatsList.Where(x => x.ScreenID == s.Number).ToList();

                //    foreach (Seat b in seatsList)
                //    {
                //        System.Diagnostics.Debug.WriteLine("Het ID is: " + b.SeatID);
                //    }
                //    //s.Seats = Context.Seats.Where(x => x.ScreenID == s.Number);
                //    //s.Seats = new Seat[] { new Seat { SeatNumber = 1}, new Seat { SeatNumber = 2} };
                //    System.Diagnostics.Debug.WriteLine(s.Seats.Count);
                //}
                //return Context.Screens;
                return Context.Screens.Include(x => x.Seats);
            }
        }
        
        //Add functionality
        public void AddScreens(Screen Screen)
        {
            Context.Screens.Add(Screen);
            Context.SaveChanges();
        }

        public IEnumerable<Seat> Seats
        {
            get { return Context.Seats; }
        }

        //Add functionality
        public void AddSeats(Seat Seat)
        {
            Context.Seats.Add(Seat);
            Context.SaveChanges();
        }

        public IEnumerable<Show> Shows
        {
            get
            {
                return Context.Shows.Include(x => x.Movie).Include(x => x.Screen).Include(x=> x.Screen.Seats).Include(x => x.Tickets);
                //return Context.Shows;
            }
        }

        public void AddShows(Show Show)
        {
            Context.Shows.Add(Show);
            Context.SaveChanges();
        }

        public IEnumerable<Tariff> Tariffs
        {
            get { return Context.Tariffs; }
        }

        public Tariff GetTariff(string Name)
        {
            return Tariffs.Where(x => x.Name.Equals(Name)).First();
        }

        public void AddTariffs(Tariff Tariff)
        {
            Context.Tariffs.Add(Tariff);
            Context.SaveChanges();
        }

        public IEnumerable<Ticket> Tickets
        {
            get { return Context.Tickets; }
        }

        //Add functionality
        public void AddTickets(Ticket Ticket)
        {
            Context.Tickets.Add(Ticket);
            Context.SaveChanges();
        }

        //Add functionality
        public void AddRating(Rating Rating)
        {
            Context.Ratings.Add(Rating);
            Context.SaveChanges();
        }
        // aan te passen
        //public void SetVacated(IEnumerable<Seat> unVacatedSeats, Show show)
        //{
        //    Context.Seats.Find(unVacatedSeats).Vacated = true;
        //}

        //public void SetUnVacated(IEnumerable<Seat> vacatedSeats, Show show)
        //{
        //    Context.Seats.Find(vacatedSeats).Vacated = true;
        //}
        
        public IEnumerable<Order> Orders
        {
            get { return Context.Orders; }
        }

        public void AddOrder(Order Order)
        {
            Context.Orders.Add(Order);
            Context.SaveChanges();
        }
        public IEnumerable<Movie> GetMovieWeekMovies()
        {   
            DateTime Today =  DateTime.Now;
            //Get the next Thursday
            DateTime NextThursday = Today.Date.AddDays(((int)Today.DayOfWeek - (int)DayOfWeek.Thursday) + 7);
            //Get the previous Thursday
            DateTime PreviousThursday = NextThursday.AddDays(-7);
            return Shows.Where(x => x.DateTime < NextThursday && x.DateTime >= PreviousThursday).Select(x => x.Movie).ToList<Movie>().Distinct();
        }

        public IEnumerable<Movie> GetMoviesFromNowMovies()
        {
            List<Movie> MoviesToBeReleased = Movies.Where(x=>x.ReleaseDate > DateTime.Today).ToList();
            List<Movie> MoviesWithShowsThisWeek = GetMovieWeekMovies().ToList();
            return MoviesWithShowsThisWeek.Concat(MoviesToBeReleased);
        }

        // Returns all unique movies for a certain date
        public IEnumerable<Movie> GetDayMovies(String Date)
        {
            return Shows.Where(x => x.DateTime.ToString("d-M-yyyy", new CultureInfo("nl-NL")) == Date && x.DateTime >= DateTime.Now).Select(x => x.Movie).ToList<Movie>().Distinct();
        }

        // Returns all shows for a movie and cinema on a certain date
        public List<Show> GetMovieShows(String Cinema, Movie Movie, String Date)
        {
            DateTime Today = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("Duurt Lang");
            List<Show> shows = Context.Shows.Where(x => x.Cinema.Name == Cinema && x.MovieID == Movie.MovieID && x.DateTime >= Today).ToList();
            return shows.Where(x => x.DateTime.ToString("d-M-yyyy", new CultureInfo("nl-NL")) == Date).ToList();
        }

        public List<Show> GetMovieShowsWeek(Movie Movie)
        {
            DateTime Today = DateTime.Now;
            //Get the next Thursday
            DateTime NextThursday = Today.Date.AddDays(((int)Today.DayOfWeek - (int)DayOfWeek.Thursday) + 7);

            return Shows.Where(x => x.DateTime >= DateTime.Today && x.DateTime < NextThursday && x.DateTime >= DateTime.Now && x.MovieID == Movie.MovieID).ToList();
        }
    }
}