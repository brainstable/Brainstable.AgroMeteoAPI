using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brainstable.AgroMeteoAPI.Entities.Models
{
    public class MeteoStation
    {
        [Column("meteo_station_id")]
        public int MeteoStationId { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "")]
        [MaxLength(100, ErrorMessage = "")]
        public string Name { get; set; }

        [Column("lat")]
        public double? Latitude { get; set; }

        [Column("lon")]
        public double? Longitude { get; set; }

        [Column("alt")]
        public double? Altitude { get; set; }

        [Column("name_rus")]
        [MaxLength(100, ErrorMessage = "")]
        public string? NameRus { get; set; }

        [Column("name_eng")]
        [MaxLength(100, ErrorMessage = "")]
        public string? NameEng { get; set; }

        [Column("country_alpha_2")]
        public string? CountryAlpha2 { get; set; }

        public ICollection<MeteoDay> MeteoDays { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
