using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.WebUI.Website.Models;

namespace IVH7_Cinema.WebUI.Website.Helpers
{
    public class Calculator
    {
        public ReviewResults CalculateAverages(List<Questionnaire> Questionnaires)
        {
            if (Questionnaires.Count() > 0)
            {
                double generalAverage = 0;
                double employeeAverage = 0;
                double filmsAverage = 0;
                double hygieneAverage = 0;
                double screenAverage = 0;
                double parkingAverage = 0;
                double siteAverage = 0;
                double foodAverage = 0;
                double priceAverage = 0;
                double buildingAverage = 0;


                //Teller initialiseren
                double questionnaireCount = 0;

                //Totale tellers initialiseren
                double generalCounter = 0;
                double employeeCounter = 0;
                double filmsCounter = 0;
                double hygieneCounter = 0;
                double screenCounter = 0;
                double parkingCounter = 0;
                double siteCounter = 0;
                double foodCounter = 0;
                double priceCounter = 0;
                double buildingCounter = 0;

                //Waarden ophalen en totalen optellen
                foreach (Questionnaire Q in Questionnaires)
                {
                    generalCounter += Q.GeneralRating;
                    employeeCounter += Q.EmployeeRating;
                    filmsCounter += Q.FilmsRating;
                    hygieneCounter += Q.HygieneRating;
                    screenCounter += Q.ScreenRating;
                    parkingCounter += Q.ParkingRating;
                    siteCounter += Q.SiteRating;
                    foodCounter += Q.FoodRating;
                    priceCounter += Q.PriceRating;
                    buildingCounter += Q.BuildingRating;

                    questionnaireCount++;
                }
                if (questionnaireCount != 0)
                {
                    //Gemiddelden berekenen
                    generalAverage = generalCounter / questionnaireCount;
                    employeeAverage = employeeCounter / questionnaireCount;
                    filmsAverage = filmsCounter / questionnaireCount;
                    hygieneAverage = hygieneCounter / questionnaireCount;
                    screenAverage = screenCounter / questionnaireCount;
                    parkingAverage = parkingCounter / questionnaireCount;
                    siteAverage = siteCounter / questionnaireCount;
                    foodAverage = foodCounter / questionnaireCount;
                    priceAverage = priceCounter / questionnaireCount;
                    buildingAverage = buildingCounter / questionnaireCount;
                }
                ReviewResults r = new ReviewResults()
                {
                    GeneralAverage = generalAverage,
                    EmployeeAverage = employeeAverage,
                    FilmsAverage = filmsAverage,
                    HygieneAverage = hygieneAverage,
                    ScreenAverage = screenAverage,
                    ParkingAverage = parkingAverage,
                    SiteAverage = siteAverage,
                    FoodAverage = foodAverage,
                    PriceAverage = priceAverage,
                    BuildingAverage = buildingAverage
                };

                return r;
            }
            else
            {
                ReviewResults f = new ReviewResults();
                return f;
            }
        }
    }
}