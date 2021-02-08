using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Csv2Json
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "sample.csv";
            if (args.Length < 1)
            {
                Console.WriteLine("No file name was given. Using sample.csv file.");
            }
            else
            {
                fileName = args[0];
            }

            var json = ConvertToJson(fileName);
            ShowJson(json);
        }

        static string ConvertToJson(string fileName)
        {
            var holidays = new Dictionary<string, Dictionary<string, string>>();
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var lastCountry = "";
                while (csv.Read())
                {
                    var country = csv[0];
                    var date = csv[1];
                    var holiday = csv[2];
                    if (!string.IsNullOrWhiteSpace(country))
                    {
                        lastCountry = country;
                        AddCountry(holidays, country);
                    }

                    if (!string.IsNullOrWhiteSpace(date))
                    {
                        AddHoliday(holidays, lastCountry, date, holiday);
                    }
                }
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(holidays);
        }

        private static void AddHoliday(Dictionary<string, Dictionary<string, string>> holidays, string lastCountry, string date, string holiday)
        {
            AddCountry(holidays, lastCountry);
            var existingHolidays = holidays[lastCountry];

            if (existingHolidays.ContainsKey(date)) return;

            existingHolidays[date] = holiday;
        }

        private static void AddCountry(Dictionary<string, Dictionary<string, string>> holidays, string country)
        {
            if (holidays.ContainsKey(country)) return;
            holidays.Add(country, new Dictionary<string, string>());
        }

        static void ShowJson(string json)
        {
            Console.WriteLine(json);
        }
    }
}
