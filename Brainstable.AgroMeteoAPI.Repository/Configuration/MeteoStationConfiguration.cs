using Brainstable.AgroMeteoAPI.Entities.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Text;

namespace Brainstable.AgroMeteoAPI.Repository.Configuration
{
    internal class MeteoStationConfiguration : IEntityTypeConfiguration<MeteoStation>
    {
        readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "meteo_stations.csv");

        public void Configure(EntityTypeBuilder<MeteoStation> builder)
        {
            IEnumerable<MeteoStation> meteoStations = LoadFromFile(filePath);
            builder.HasData(meteoStations);
        }

        private IEnumerable<MeteoStation> LoadFromFile(string filePath)
        {
            List<MeteoStation> meteoStations = new List<MeteoStation>();

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] arr = lines[i].Split(';');

                MeteoStation meteoStation = new MeteoStation
                {
                    MeteoStationId = arr[0],
                    Name = arr[1],
                    Latitude = !string.IsNullOrWhiteSpace(arr[2]) ? Convert.ToDouble(arr[2].Replace('.', ',')) : null,
                    Longitude = !string.IsNullOrWhiteSpace(arr[3]) ? Convert.ToDouble(arr[3].Replace('.', ',')) : null,
                    Altitude = !string.IsNullOrWhiteSpace(arr[4]) ? Convert.ToDouble(arr[4].Replace('.', ',')) : null,
                    Country = !string.IsNullOrWhiteSpace(arr[5]) ? arr[5] : null,
                    NameRus = !string.IsNullOrWhiteSpace(arr[6]) ? arr[6] : null,
                    NameEng = !string.IsNullOrWhiteSpace(arr[7]) ? arr[7] : null,
                };

                meteoStations.Add(meteoStation);
            }

            return meteoStations;
        }
    }
}
