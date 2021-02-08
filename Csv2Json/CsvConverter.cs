using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace Csv2Json
{
    public static class CsvConverter
    {
        private const string ConcatenatorString = ",";
        public static string ConvertToJson(string fileName)
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
                    AddObject(objectCollection, objectName);
                }

                if (!string.IsNullOrWhiteSpace(propertyName))
                {
                    AddProperty(objectCollection, lastObjectName, propertyName, propertyValue);
                }
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(objectCollection);
        }

        private static void AddProperty(Dictionary<string, Dictionary<string, string>> objectCollection, 
                                        string objectName, 
                                        string propertyName, 
                                        string propertyValue)
        {
            EnsureObjectName(objectCollection, objectName);
            var existingProperties = objectCollection[objectName];

            if (existingProperties.ContainsKey(propertyName))
            {
                var existingValue = existingProperties[propertyName];
                var concatenatedValue = $"{existingValue}{ConcatenatorString}{propertyValue}";
                existingProperties[propertyName] = concatenatedValue;
            }
            else
            {
                existingProperties[propertyName] = propertyValue;
            }
        }

        private static void EnsureObjectName(Dictionary<string, Dictionary<string, string>> objectCollection, string objectName)
        {
            AddObject(objectCollection, objectName);
        }

        private static void AddObject(Dictionary<string, Dictionary<string, string>> objectCollection, string objectName)
        {
            if (objectCollection.ContainsKey(objectName)) return;
            objectCollection.Add(objectName, new Dictionary<string, string>());
        }
    }
}