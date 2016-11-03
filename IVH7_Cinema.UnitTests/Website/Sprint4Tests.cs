using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using IVH7_Cinema.WebUI.Website.Controllers;
using IVH7_Cinema.WebUI.Website.Models;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.Domain.Abstract;
using IVH7_Cinema.WebUI.Website.Helpers;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using System.Linq;
using System.Globalization;
using WebMatrix.WebData;
using System.Web.Security;
using System.IO;
namespace IVH7_Cinema.UnitTests.Website
{
    [TestClass]
    public class Sprint4Tests
    {
        Mock<ICinemaRepository> RepMock = new Mock<ICinemaRepository>();
        Mock<IMailer> MailMock = new Mock<IMailer>();


        //Lost and found tests
        [TestMethod]
        public void TestObjectsReturn()
        {
            //Arrange - create objects list
            List<LostObject> objects = new List<LostObject>() { 
                new LostObject{Name = "Adidas pet", EmailAddress = "richarddejongh@home.nl", FinderAddress="teststraat", FinderEmail="frits@frits.nl", FinderName="Frits Frederiks", Foundplace="WC", PickedUp = false, DateTime= Convert.ToDateTime("02-01-2001") },
                new LostObject{ Name = "Wc borstel", EmailAddress="richarddejongh1995@gmail.com", FinderAddress = "testraat.nl", FinderEmail="henk@henkie.nl", FinderName="Henk Henksen", Foundplace="Zaal 5", PickedUp = true, DateTime = Convert.ToDateTime("03-02-2001")}
            };

            // Arrange - Set the mock
            RepMock.Setup(x => x.LostObjects).Returns(objects);


            //Act
            int i = RepMock.Object.LostObjects.Count();

            //Assert
            Assert.AreEqual(i, 2);
        }

        [TestMethod]
        public void ReviewsPerMovieViewTest()
        {
            //Arrange - create objects list
            Movie m1 = new Movie { MovieID = 1, Title = "Gooise Vrouwen", Duration = 120, Genres = new List<Genre> { new Genre { Name = "Women" } }, Is3DAvailable = false };
            Movie m2 = new Movie { MovieID = 2, Title = "50 shades of grey", Duration = 130, Genres = new List<Genre> { new Genre { Name = "Erotic" } }, Is3DAvailable = false };
            Movie m3 = new Movie { MovieID = 3, Title = "Imitation Game", Duration = 110, Genres = new List<Genre> { new Genre { Name = "Action" } }, Is3DAvailable = true };


            List<Movie> Movies = new List<Movie>
                { m1, m2, m3
                     };
            MovieReview r1 = new MovieReview() { ReviewID = 1, Comments = "rotzooi", Movie = m1, Rating = 2 };

            MovieReview r2 = new MovieReview() { ReviewID = 2, Comments = "leuk", Movie = m1, Rating = 4 };
            MovieReview r3 = new MovieReview() { ReviewID = 3, Comments = "ging wel", Movie = m2, Rating = 3 };
            List<MovieReview> reviews = new List<MovieReview>(){
                r1,r2, r3
            };
            RepMock.Setup(x => x.Reviews).Returns(reviews);

            AccountController target = new AccountController(RepMock.Object, MailMock.Object);
            var result = target.ReviewsPerMovie(1) as ViewResult;


            //Assert
            Assert.AreEqual("ReviewsPerMovie", result.ViewName);


        }

        [TestMethod]
        public void CalculateAveragesTest()
        {
            //Arrange - create objects list
            List<Questionnaire> questionnaires = new List<Questionnaire>
                {
                    new Questionnaire(){
                        BuildingRating = 1, EmployeeRating = 3, FilmsRating = 4, FoodRating = 3, GeneralRating = 4, HygieneRating = 2, ParkingRating = 2, PriceRating = 2, ScreenRating = 5, SiteRating = 5
}, new Questionnaire(){
                        BuildingRating = 4, EmployeeRating = 2, FilmsRating = 2, FoodRating = 1, GeneralRating = 2, HygieneRating = 4, ParkingRating = 3, PriceRating = 1, ScreenRating = 2, SiteRating = 1
}
                     };

            RepMock.Setup(x => x.Questionnaires).Returns(questionnaires);

            Calculator c = new Calculator();
           ReviewResults result = c.CalculateAverages(RepMock.Object.Questionnaires.ToList<Questionnaire>());

            //Assert
            Assert.AreEqual(2.50, result.BuildingAverage);


        }
    }
}
