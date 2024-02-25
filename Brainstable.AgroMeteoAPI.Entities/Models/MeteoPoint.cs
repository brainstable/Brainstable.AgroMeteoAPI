using System.ComponentModel.DataAnnotations.Schema;

namespace Brainstable.AgroMeteoAPI.Entities.Models
{
    [Table("meteo_points")]
    public class MeteoPoint
    {
        [Column("meteo_station_id")]
        [ForeignKey(nameof(MeteoStation))]
        public string MeteoStationId { get; set; }

        public MeteoStation? MeteoStation { get; set; }

        [Column("date")]
        public DateOnly Date { get; set; }

        [Column("t")]
        public double? Temperature { get; set; }

        [Column("tmin")]
        public double? MinTemperature { get; set; }

        [Column("tmax")]
        public double? MaxTemperature { get; set; }

        [Column("r")]
        public double? Rainfall { get; set; }

        [Column("sh")]
        public double? SnowHight { get; set; }

        [Column("h")]
        public double? Humidity { get; set; }

        public override string ToString()
        {
            return $"{MeteoStationId} - {Date.ToShortDateString()}";
        }
    }
}
