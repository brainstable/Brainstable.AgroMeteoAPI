using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brainstable.AgroMeteoAPI.Entities.Models
{
    [Table("meteo_points")]
    public class MeteoPoint
    {
        [Column("meteo_station_id")]
        [ForeignKey(nameof(MeteoStation))]
        [Required]
        public string MeteoStationId { get; set; }

        public MeteoStation? MeteoStation { get; set; }

        [Column("date")]
        [Required]
        public DateOnly Date { get; set; }

        [Column("t")]
        [Range(-100.0, 100.0, ErrorMessage = "")]
        public double? Temperature { get; set; }

        [Column("tmin")]
        [Range(-100.0, 100.0, ErrorMessage = "")]
        public double? MinTemperature { get; set; }

        [Column("tmax")]
        [Range(-100.0, 100.0, ErrorMessage = "")]
        public double? MaxTemperature { get; set; }

        [Column("r")]
        [Range(0.0, 10000.0, ErrorMessage = "")]
        public double? Rainfall { get; set; }

        [Column("sh")]
        [Range(0.0, 10000.0, ErrorMessage = "")]
        public double? SnowHight { get; set; }

        [Column("h")]
        [Range(0.0, 100.0, ErrorMessage = "")]
        public double? Humidity { get; set; }

        public override string ToString()
        {
            return $"{MeteoStationId} - {Date.ToShortDateString()}";
        }
    }
}
