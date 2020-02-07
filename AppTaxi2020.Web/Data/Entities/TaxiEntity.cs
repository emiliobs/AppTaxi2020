using System.ComponentModel.DataAnnotations;

namespace AppTaxi2020.Web.Data.Entities
{
    public class TaxiEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(6, MinimumLength = 6, ErrorMessage = "The {0} field must have {1} character.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Plaque { get; set; }
    }
}
