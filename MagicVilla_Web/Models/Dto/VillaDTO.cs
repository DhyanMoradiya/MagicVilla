using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto
{
    public class VillaDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public int Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }

        public string ImageUrl { get; set; }
        public string Amenity { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
