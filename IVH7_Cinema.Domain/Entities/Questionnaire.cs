using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVH7_Cinema.Domain.Entities
{
    public class Questionnaire
    {
        public Questionnaire(){
            Time = DateTime.Now;
    }
        [Key]
        public int Id { get; set; }
        [Required]
        public int GeneralRating { get; set; }

        [Required]
        public int EmployeeRating { get; set; }

        [Required]
        public int FilmsRating { get; set; }

        [Required]
        public int HygieneRating { get; set; }

        [Required]
        public int ScreenRating { get; set; }

        [Required]
        public int ParkingRating { get; set; }

        [Required]
        public int SiteRating { get; set; }

        [Required]
        public int FoodRating { get; set; }

        [Required]
        public int PriceRating { get; set; }

        [Required]
        public int BuildingRating { get; set; }

        
        public DateTime Time { get; set; }
    }
}
