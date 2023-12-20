using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Model
{
    public class Villa
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }

        public string ImageUrl { get; set; }
        public string Amenity { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
