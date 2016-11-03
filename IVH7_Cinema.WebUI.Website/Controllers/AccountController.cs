using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVH7_Cinema.WebUI.Website.Models;
using WebMatrix.WebData;
using System.Web.Security;
using IVH7_Cinema.Domain.Abstract;
using IVH7_Cinema.Domain.Entities;
using System.IO;
using System.Globalization;
using IVH7_Cinema.WebUI.Website.Helpers;

namespace IVH7_Cinema.WebUI.Website.Controllers
{
    public class AccountController : BaseController
    {

        ICinemaRepository Repository;
        IMailer Emailer;
        char[] commaSeparator = new char[] { ',' };

        public AccountController(ICinemaRepository cinRep, IMailer mailer)
        {
            Repository = cinRep;
            Emailer = mailer;
            ViewData["Cinemas"] = Repository.Cinemas.Select(x => new Cinema { CinemaID = x.CinemaID, Email = x.Email, Phone = x.Phone, Name = x.Name, Address = x.Address, ZipCode = x.ZipCode, City = x.City }).OrderBy(x => x.City).ThenBy(x => x.Name).ToList();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login Logindata, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(Logindata.Username, Logindata.Password))
                {
                    if (ReturnUrl != null)
                    {

                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index");
                }
                else
                {

                    ModelState.AddModelError("", "Sorry, gebruikersnaam en/of wachtwoord is incorrect");
                    return View(Logindata);
                }


            }
            ModelState.AddModelError("", "Sorry, gebruikersnaam en/of wachtwoord is incorrect");
            return View(Logindata);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register Registerdata, string Role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(Registerdata.Username, Registerdata.Password);
                    Roles.AddUserToRole(Registerdata.Username, Role);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException Exception)
                {
                    ModelState.AddModelError("", "Sorry de gebruikersnaam bestaat al");
                    return View(Registerdata);
                }
            }
            ModelState.AddModelError("", "Sorry de gebruikersnaam bestaat al");
            return View(Registerdata);
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "back-office")]
        [HttpGet]
        public ActionResult AddMovie()
        {
            MoviesAddViewModel model = new MoviesAddViewModel()
            {
                Movie = new Movie(),
                Genres = Repository.Genres.ToList<Genre>(),
                Cinemas = Repository.Cinemas.ToList<Cinema>(),
                Languages = Repository.Languages.ToList<Language>(),
                Ratings = Repository.Ratings.ToList<Rating>()
            };
            Movie s = new Movie();
            return View(s);
        }

        [Authorize(Roles = "back-office")]
        [HttpPost]
        public ActionResult AddMovie(Movie M, HttpPostedFileBase File, HttpPostedFileBase File2)
        {
            if (ModelState.IsValid)
            {
                if (File.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/MovieCovers"), fileName);
                    File.SaveAs(path);

                }

                if (File2.ContentLength > 0)
                {
                    var fileName2 = Path.GetFileName(File2.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/Content/MovieBanners"), fileName2);
                    File2.SaveAs(path2);

                }
                M.ImageURL = File.FileName;
                M.BannerURL = File2.FileName;

                //Genres
                List<string> genreList = new List<string>();
                List<Genre> finalGenreList = new List<Genre>();

                string genre = Request["Genre.GenreName"];
                string[] result = genre.Split(commaSeparator, StringSplitOptions.None);
                foreach (string Str in result)
                {
                    genreList.Add(Str);
                }

                foreach (string Gen in genreList)
                {
                    foreach (Genre G in Repository.Genres.ToList<Genre>())
                    {
                        if (G.Name.Equals(Gen))
                        {
                            finalGenreList.Add(G);
                        }

                    }
                }

                //Languages
                List<string> languageList = new List<string>();
                List<Language> finalLanguageList = new List<Language>();

                string language = Request["Language.LanguageName"];
                string[] languageresult = language.Split(commaSeparator, StringSplitOptions.None);
                foreach (string Str in languageresult)
                {
                    languageList.Add(Str);
                }

                foreach (string Lan in languageList)
                {
                    foreach (Language L in Repository.Languages.ToList<Language>())
                    {
                        if (L.LanguageName.Equals(Lan))
                        {
                            finalLanguageList.Add(L);
                        }

                    }
                }
                //Cinemas
                List<string> cinemaList = new List<string>();
                List<Cinema> finalCinemaList = new List<Cinema>();

                string cinema = Request["Cinema.CinemaName"];
                string[] cinemaresult = cinema.Split(commaSeparator, StringSplitOptions.None);
                foreach (string Str in cinemaresult)
                {
                    cinemaList.Add(Str);
                }

                foreach (string Cin in cinemaList)
                {
                    foreach (Cinema C in Repository.Cinemas.ToList<Cinema>())
                    {
                        if (C.Name.Equals(Cin))
                        {
                            finalCinemaList.Add(C);
                        }

                    }
                }

                //Ratings
                List<string> ratingList = new List<string>();
                List<Rating> finalRatingList = new List<Rating>();

                string rating = Request["Rating.RatingName"];
                string[] ratingresult = rating.Split(commaSeparator, StringSplitOptions.None);
                foreach (string Str in ratingresult)
                {
                    ratingList.Add(Str);
                }

                foreach (string Rat in ratingList)
                {
                    foreach (Rating R in Repository.Ratings.ToList<Rating>())
                    {
                        if (R.Name.Equals(Rat))
                        {
                            finalRatingList.Add(R);
                        }
                        //else
                        //{
                        //    Rating ratingg = new Rating() { Name = rat };
                        //    Repository.AddRating(ratingg);
                        //    FinalRatingList.Add(ratingg);
                        //}
                    }
                }

                M.Genres = finalGenreList;
                M.Cinemas = finalCinemaList;
                M.Languages = finalLanguageList;
                M.Ratings = finalRatingList;
                Repository.AddMovies(M);
                return View("AddedMovie", M);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "back-office")]
        [HttpGet]
        public ActionResult SendNewsletter()
        {
            return View();
        }

        [Authorize(Roles = "back-office")]
        [HttpPost]
        public ActionResult SendNewsletter(HttpPostedFileBase File)
        {
            if (File != null)
            {
                try
                {
                    foreach (Subscriber S in Repository.Subscribers)
                    {
                        Emailer.SendMessage(S, File);
                    }
                    TempData["Message"] = "Bericht succesvol verzonden!";
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                TempData["FailMessage"] = "Er is geen bestand geselecteerd";
                return View();
            }
            return View();
        }

        [Authorize(Roles = "kassa")]
        public ActionResult SellTickets(Int64 ShowID = 0, int Page = 1)
        {
            ViewBag.ShowID = ShowID;
            ViewBag.Page = Page;

            ShowsListViewModel model = new ShowsListViewModel
            {
                Shows = Repository.Shows.Where(s => s.DateTime >= DateTime.Now && s.DateTime < DateTime.Today.AddDays(1) && s.CinemaID == 1).OrderBy(s => s.DateTime.ToString("HH:mm", new CultureInfo("nl-NL"))),
                Movies = Repository.GetDayMovies(DateTime.Today.ToString("d-M-yyyy", new CultureInfo("nl-NL"))).ToList(),
                AllMovies = Repository.Movies

            };

            return View(model);
        }

        [Authorize(Roles = "kassa")]
        public ActionResult LostAndFound()
        {
            ObjectViewModel model = new ObjectViewModel()
            {
                objects = Repository.LostObjects.ToList<LostObject>()
            };
            return View(model);
        }

        [Authorize(Roles = "kassa")]
        [HttpGet]
        public ActionResult NewLostObject()
        {
            return View("LostAndFound");
        }

        [Authorize(Roles = "kassa")]
        [HttpPost]
        public ActionResult NewLostObject(LostObject LostObject)
        {
            if (ModelState.IsValid)
            {
                Repository.AddLostObject(LostObject);
                return View("LostObjectSummary", LostObject);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "kassa")]
        public ActionResult ObjectSummary(LostObject LostObject)
        {
            return View("LostObjectSummary", LostObject);
        }

        [Authorize(Roles = "kassa")]
        [HttpGet]
        public ActionResult EditObject(LostObject Lost)
        {
            return View(Lost);
        }

        [Authorize(Roles = "kassa")]
        [HttpPost]
        public ActionResult Edit(LostObject Lostobject)
        {
            if (ModelState.IsValid)
            {
                Repository.ChangeLostObject(Lostobject);
                TempData["ObjectMessage"] = "Object succesvol gewijzigd!";
                return View("LostObjectSummary", Lostobject);
            }
            TempData["ObjectMessage"] = "Probeer het nogmaals!";

            return View("EditObject", Lostobject);
        }

        [Authorize(Roles = "kassa")]
        public ActionResult ChangePickedUp(LostObject LostObject)
        {
            LostObject.PickedUp = true;
            Repository.ChangeLostObject(LostObject);
            TempData["PickedMessage"] = "Bedankt voor het ophalen. De adresgegevens van de vinder staan hieronder. Stuur eens een bedankje!";
            return View("LostObjectSummary", LostObject);
        }

        [HttpGet]
        public ActionResult Questionnaire(Questionnaire Q)
        {
            return View(Q);
        }

        [HttpPost]
        public ActionResult QuestionnaireAdd(Questionnaire Q)
        {
            if (ModelState.IsValid)
            {
                Repository.AddQuestionnaire(Q);
                TempData["QuestionnaireMessage"] = "Bedankt voor je mening! Wij streven ernaar om onze bioscoop continu te verbeteren";
                return View("Questionnaire", Q);
            }

            TempData["QuestionnaireFailureMessage"] = "Niet alle velden zijn ingevuld. Probeer het nogmaals!";
            return RedirectToAction("Questionnaire");

        }

        [Authorize(Roles = "manager")]
        public ActionResult QuestionnaireResults()
        {
            List<Questionnaire> questionnaires = new List<Questionnaire>();
            questionnaires = Repository.Questionnaires.ToList<Questionnaire>();

            //Gemiddelde varaibelen initialiseren
            Calculator c = new Calculator();
            ReviewResults r = c.CalculateAverages(questionnaires);
            return View(r);
        }

        public ActionResult MovieReview()
        {
            MovieReviewModel model = new MovieReviewModel()
            {
                ReviewedMovies = Repository.Movies
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult AddMovieReview()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovieReview(MovieReview R, FormCollection Form)
        {
            R.Movie = Repository.GetMovie(Convert.ToInt32(Form["Movie"]));
            R.Comments = Form["review"];
            Repository.AddMovieReview(R);
            TempData["ReviewMessage"] = "Bedankt voor je mening!";
            ViewBag.MovieTitle = R.Movie.Title;
            List<MovieReview> reviews = new List<MovieReview>();
            foreach (MovieReview F in Repository.Reviews.ToList<MovieReview>())
            {
                if (F.Movie.MovieID.Equals(R.Movie.MovieID))
                {
                    reviews.Add(F);
                }
            }
            return View("ReviewsPerMovie", reviews);
        }

        public ActionResult ReviewsPerMovie(long MovieID)
        {
            List<MovieReview> reviews = new List<MovieReview>();
            foreach (MovieReview F in Repository.Reviews.ToList<MovieReview>())
            {
                if (F.Movie.MovieID.Equals(MovieID))
                {
                    reviews.Add(F);
                }
            }
            ViewBag.MovieID = MovieID;
            return View("ReviewsPerMovie", reviews);
        }
    }
}