using System.ComponentModel.DataAnnotations;

namespace AppTaxi2020.Web.Data.Entities
{
    public class TaxiEntity
    {
        [Key]
        public int Id { get; set; }

        [RegularExpression(@"^([A-Za-z]{3}\d{3})$", ErrorMessage = "The {0} field must have {1} character.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "The field {0} must start three character and ends with numbers.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Plaque { get; set; }
    }
}
