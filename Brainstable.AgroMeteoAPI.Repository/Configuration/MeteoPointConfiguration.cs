using Brainstable.AgroMeteoAPI.Entities.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Specialized;

namespace Brainstable.AgroMeteoAPI.Repository.Configuration
{
    internal class MeteoPointConfiguration : IEntityTypeConfiguration<MeteoPoint>
    {
        readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "29766.01.02.2005.30.09.2023.1.0.0.ru.ansi.00000000.csv");

        public void Configure(EntityTypeBuilder<MeteoPoint> builder)
        {
            IEnumerable<MeteoPoint> meteoStations = LoadFromFile(filePath);
            builder.HasData(meteoStations);
        }

        const double LIMIT_RAIN = 150;

        private IEnumerable<MeteoPoint> LoadFromFile(string filePath)
        {
            List<MeteoPoint> meteoPoints = new List<MeteoPoint>();

            string[] lines = File.ReadAllLines(filePath);

            NameValueCollection collTemp = new NameValueCollection();
            NameValueCollection collTempMin = new NameValueCollection();
            NameValueCollection collTempMax = new NameValueCollection();
            NameValueCollection collRain = new NameValueCollection();
            NameValueCollection collSnow = new NameValueCollection();
            NameValueCollection collRH = new NameValueCollection();

            for (int i = 7; i < lines.Length; i++)
            {
                string id = lines[i].Substring(1, 10).Trim();
                string[] arr = lines[i].Split('\"');

                List<string> list = new List<string>();

                for (int j = 0; j < arr.Length; j++)
                {
                    if (j != 0)
                    {
                        if (arr[j].Trim() != ";")
                            list.Add(arr[j]);
                    }
                }

                double rh;
                if (!Double.TryParse(list[5].Replace('\"', ' ').Replace('.', ',').Trim(), out rh))
                {
                    rh = 0.0;
                }

                double dp;
                if (!Double.TryParse(list[22].Replace('\"', ' ').Replace('.', ',').Trim(), out dp))
                {
                    dp = 0.0;
                }

                double rain;
                if (!Double.TryParse(list[23].Replace('\"', ' ').Replace('.', ',').Trim(), out rain))
                {
                    rain = 0.0;
                }
                if (rain > LIMIT_RAIN)
                    rain = Math.Round(rain / 100, 1);


                double snow;
                if (!Double.TryParse(list[28].Replace('\"', ' ').Replace('.', ',').Trim(), out snow))
                {
                    snow = 0.0;
                }

                collTemp.Add(id, list[1].Replace('\"', ' ').Replace('.', ',').Trim());
                collRH.Add(id, rh.ToString());
                collTempMin.Add(id, list[14].Replace('\"', ' ').Replace('.', ',').Trim());
                collTempMax.Add(id, list[15].Replace('\"', ' ').Replace('.', ',').Trim());
                collRain.Add(id, rain.ToString());
                collSnow.Add(id, snow.ToString());

                if (list.Count != 29)
                    Console.WriteLine(list.Count);
            }

            int n = 0;
            foreach (string key in collTemp.Keys)
            {
                string[] arr = collTemp.GetValues(key);
                MeteoPoint point = new MeteoPoint();
                point.MeteoStationId = "29766";
                DateTime dt = Convert.ToDateTime(key);
                point.Date = new DateOnly(dt.Year, dt.Month, dt.Day);
                double sum = 0;
                double max = -100;
                double min = 100;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(arr[i]))
                    {
                        double d = Convert.ToDouble(arr[i]);
                        sum += d;
                        if (d > max)
                            max = d;
                        if (d < min)
                            min = d;
                    }
                }
                point.Temperature = Math.Round(sum / arr.Length, 1);
                point.MaxTemperature = max;
                point.MinTemperature = min;

                arr = collRH.GetValues(key);
                min = 100.0;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(arr[i]))
                    {
                        double d = Convert.ToDouble(arr[i]);
                        if (d <= min)
                            min = d;
                    }
                }
                point.Humidity = min;

                arr = collRain.GetValues(key);
                sum = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    double d = Convert.ToDouble(arr[i]);
                    sum += d;
                }
                point.Rainfall = sum;

                arr = collSnow.GetValues(key);
                sum = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    double d = Convert.ToDouble(arr[i]);
                    sum += d;
                }
                point.SnowHight = sum;

                meteoPoints.Add(point);
            }

            ValidateHighSnow(meteoPoints);

            return meteoPoints;
        }

        private void ValidateHighSnow(List<MeteoPoint> list)
        {
            for (int i = 1; i < list.Count - 1; i++)
            {
                if (list[i].SnowHight.Value == 0)
                {
                    if (list[i - 1].SnowHight.Value > 0 && list[i + 1].SnowHight.Value > 0)
                    {
                        list[i].SnowHight = list[i - 1].SnowHight;
                    }
                }
            }
        }
    }
}
