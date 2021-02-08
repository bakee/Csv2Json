using System;

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
                var json = CsvConverter.ConvertToJson(fileName);
                ShowJson(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ShowJson(string json)
        {
            Console.WriteLine(json);
        }
    }
}
