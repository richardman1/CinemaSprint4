using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVH7_Cinema.Domain.Entities
{
    public class LostObject
    {
        [Required][Key]
        public string Name { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public string Foundplace { get; set; }

        
        public string FinderName { get; set; }

        public string FinderAddress { get; set; }

        
        public string FinderEmail { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public bool PickedUp { get; set; }
    }
}
