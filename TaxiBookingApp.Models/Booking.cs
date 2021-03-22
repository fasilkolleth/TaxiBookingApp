using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaxiBookingApp.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserID { get; set; }
        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Booking Type")]
        public string BookingType { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Display(Name = "Location")]
        public string LocationDescription { get; set; }

        [Required]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Required]
        [Display(Name = "Booking Time")]
        public string BookingTime { get; set; }


        [Display(Name = "Emirates ID")]
        public string EmiratesID { get; set; }

        [Display(Name = "Status")]
        public string BookingStatus { get; set; }
    }
}
