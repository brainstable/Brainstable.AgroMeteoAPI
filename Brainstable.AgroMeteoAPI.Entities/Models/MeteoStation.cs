using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brainstable.AgroMeteoAPI.Entities.Models
{
    [Table("meteo_stations")]
    public class MeteoStation
    {
        [Column("meteo_station_id")]
        [Required(ErrorMessage = "MeteoStationId is a required field")]
        [MaxLength(6, ErrorMessage = "Maximum length for the MeteoStationId is 6 characters")]
        public string MeteoStationId { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name is a required field")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters")]
        public string Name { get; set; }

        [Column("lat")]
        public double? Latitude { get; set; }

        [Column("lon")]
        public double? Longitude { get; set; }

        [Column("alt")]
        public double? Altitude { get; set; }

        [Column("name_rus")]
        [MaxLength(100, ErrorMessage = "Maximum length for the NameRus is 100 characters")]
        public string? NameRus { get; set; }

        [Column("name_eng")]
        [MaxLength(100, ErrorMessage = "Maximum length for the NameEng is 100 characters")]
        public string? NameEng { get; set; }

        [Column("country")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Country is 50 characters")]
        public string? Country { get; set; }

        public ICollection<MeteoPoint> MeteoPoints { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
