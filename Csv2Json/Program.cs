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
                Console.WriteLine($"No file name was given. Using {fileName} file.");
            }
            else
            {
                fileName = args[0];
            }

            try
            {
                var json = ConvertToJson(fileName);
                ShowJson(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static string ConvertToJson(string fileName)
        {
            var objectCollection = new Dictionary<string, Dictionary<string, string>>();
            using var reader = new StreamReader(fileName);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var lastObjectName = "";
            while (csv.Read())
            {
                var objectName = csv[0];
                var propertyName = csv[1];
                var propertyValue = csv[2];
                if (!string.IsNullOrWhiteSpace(objectName))
                {
                    lastObjectName = objectName;
                    AddCountry(objectCollection, objectName);
                }

                if (!string.IsNullOrWhiteSpace(propertyName))
                {
                    AddHoliday(objectCollection, lastObjectName, propertyName, propertyValue);
                }
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(objectCollection);
        }

        private static void AddHoliday(Dictionary<string, Dictionary<string, string>> objectCollection, string objectName, string propertyName, string propertyValue)
        {
            AddCountry(objectCollection, objectName);
            var existingHolidays = objectCollection[objectName];

            if (existingHolidays.ContainsKey(propertyName)) return;

            existingHolidays[propertyName] = propertyValue;
        }

        private static void AddCountry(Dictionary<string, Dictionary<string, string>> objectCollection, string objectName)
        {
            if (objectCollection.ContainsKey(objectName)) return;
            objectCollection.Add(objectName, new Dictionary<string, string>());
        }

        static void ShowJson(string json)
        {
            Console.WriteLine(json);
        }
    }
}
