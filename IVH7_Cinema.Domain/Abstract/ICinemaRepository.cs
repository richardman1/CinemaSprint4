using System.Collections.Generic;
using IVH7_Cinema.Domain.Entities;
using System;

namespace IVH7_Cinema.Domain.Abstract {
    public interface ICinemaRepository {
        IEnumerable<Movie> Movies { get; }
        IEnumerable<Seat> Seats { get; }
        IEnumerable<Cinema> Cinemas { get; }
        IEnumerable<Screen> Screens { get; }
        IEnumerable<Show> Shows { get; }
        IEnumerable<Tariff> Tariffs { get; }
        IEnumerable<Ticket> Tickets { get; }
        IEnumerable<Rating> Ratings { get; }
        IEnumerable<Language> Languages { get; }
        IEnumerable<Genre> Genres { get; }
        IEnumerable<Order> Orders { get; }
        IEnumerable<Movie> GetMovieWeekMovies();
        IEnumerable<Movie> GetMoviesFromNowMovies();
        IEnumerable<Movie> GetDayMovies(String Date);
        List<Show> GetMovieShows(String Cinema, Movie Movie, String Date);
        List<Show> GetMovieShowsWeek(Movie Movie);
        IEnumerable<LostObject> LostObjects{get;}
        IEnumerable<Subscriber> Subscribers { get; }
        IEnumerable<Questionnaire> Questionnaires { get; }
        IEnumerable<MovieReview> Reviews { get; }

        void AddRating(Rating Rating);
        void AddLanguage(Language Language);
        void AddMovies(Movie Movie);
        void AddCinemas(Cinema Cinema);
        void AddScreens(Screen Screen);
        void AddGenre(Genre Genre);
        void AddSeats(Seat Seat);
        void AddShows(Show Show);
        void AddTariffs(Tariff Tariff);
        void AddTickets(Ticket Ticket);
        void AddOrder(Order Order);
        void AddLostObject(LostObject LostObject);
        void AddMovieReview(MovieReview R);
        void ChangeOrderStatus(Int64 OrderID, string Status);
        void ChangeLostObject(LostObject Lostobject);
        void AddSubscriber(Subscriber Subscriber);
        void AddQuestionnaire(Questionnaire Q);
        //void SetVacated(IEnumerable<Seat> unVacatedSeats, Show show);
        //void SetUnVacated(IEnumerable<Seat> VacatedSeats, Show show);

        void DeleteSubscriber(Subscriber Subscriber);

        Cinema GetCinema(string Name);

        Movie GetMovie(Int64 ID);

        Screen GetScreen(Int64 Number);

        Tariff GetTariff(string Name);

        Order GetOrder(Int64 ID);

        void DeleteOrder(Int64 OrderID);
    }
}
