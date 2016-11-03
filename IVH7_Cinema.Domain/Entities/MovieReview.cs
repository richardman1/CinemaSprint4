using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IVH7_Cinema.Domain.Entities
{
    public class MovieReview
    {
        [Key]
        public int ReviewID { get; set; }
        [Required]
        public int Rating { get; set; }

        public string Comments { get; set; }
        [Required]
        public virtual Movie Movie { get; set; }
    }
}
