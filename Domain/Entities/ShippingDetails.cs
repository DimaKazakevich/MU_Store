using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ShippingDetails
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Fisrt Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Filed is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Addres Line")]
        public string AddresLine { get; set; }

        [Required]
        [Display(Name = "Town")]
        public string Town { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        public string UserId { get; set; }
    }
}
