/// Programming Excercise
/// Project: EmployeeTimeExercise
/// Company: IOET Inc.
/// Applicant: Emmanuel Reyes
/// Date: May 28, 2022.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeTimeExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Validate source file
                var fileLocation = ConfigurationManager.AppSettings["sourceFileLocation"];

                if (String.IsNullOrEmpty(fileLocation))
                    throw new Exception("No 'sourceFileLocation' value in config file");


                if (!File.Exists(fileLocation))
                    throw new Exception("The source file could not be found");

                //Read and Parse file
                string[] fileContent = File.ReadAllLines(fileLocation);

                var parseResult = ParseFile(fileContent);


                //Get results

                if (parseResult.TimeInput == null || parseResult.TimeInput.Count == 0)
                    Console.WriteLine("No data for calculations");

                var result = Pairs(parseResult.TimeInput);

                if (result == null || result.Count == 0)
                    Console.WriteLine("There is no employees with the same time records");

                foreach (var line in result)
                    Console.WriteLine(line);

                if (parseResult.Error != null)
                    Console.WriteLine("Errors: " + parseResult.Error);


                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }

        private static List<string> Pairs(Dictionary<string, List<string>> TimeInput)
        {
            //Get all posible pairs

            List<string> pairs = new List<string>();


            foreach (var a in TimeInput)
            {
                foreach (var b in TimeInput)
                {
                    if (a.Key == b.Key || pairs.Where(x => x.Contains(a.Key + "-" + b.Key) == true).Count() > 0 || pairs.Where(x => x.Contains(b.Key + "-" + a.Key) == true).Count() > 0)
                        continue;

                    //Get intersected days
                    int counter = 0;

                    var days = a.Value.Select(s => s.Substring(0, 2)).Intersect(b.Value.Select(s => s.Substring(0, 2)));

                    foreach (var myDay in days)
                    {
                        var aRange = a.Value.Where(w => w.Substring(0, 2) == myDay).Select(s => s.Substring(2)).FirstOrDefault().Split('-');
                        var bRange = b.Value.Where(w => w.Substring(0, 2) == myDay).Select(s => s.Substring(2)).FirstOrDefault().Split('-');

                        //Check if there is an overlaping
                        var aStart = DateTime.ParseExact(aRange[0], "HH:mm", CultureInfo.InvariantCulture);
                        var aEnd = DateTime.ParseExact(aRange[1], "HH:mm", CultureInfo.InvariantCulture);

                        var bStart = DateTime.ParseExact(bRange[0], "HH:mm", CultureInfo.InvariantCulture);
                        var bEnd = DateTime.ParseExact(bRange[1], "HH:mm", CultureInfo.InvariantCulture);

                        if (aStart < bEnd && bStart < aEnd)
                            counter++;

                    }

                    pairs.Add(a.Key + "-" + b.Key + ": " + counter.ToString());


                }
            }

            return pairs;

        }

        private static ReadResult ParseFile(string[] fileContent)
        {

            if (fileContent == null)
                throw new Exception("'fileContent' is null");

            var result = new ReadResult()
            {
                TimeInput = new Dictionary<string, List<string>>()
            };
            int counter = 0;


            foreach (var line in fileContent)
            {

                try
                {
                    counter++;

                    if (!line.Contains("="))
                        throw new Exception($"Line {counter.ToString()} can not be parsed");

                    if (line.Where(x => (x == '=')).Count() > 1)
                        throw new Exception($"Line {counter.ToString()} can not be parsed");

                    var lineContent = line.Split('=');

                    var name = lineContent[0];
                    var timeItems = lineContent[1];

                    if (!timeItems.Contains(","))
                        throw new Exception($"Line {counter.ToString()} can not be parsed");

                    var timeItemsList = timeItems.Split(',').ToList();


                    result.TimeInput.Add(name, timeItemsList);
                }
                catch (Exception ex)
                {
                    result.Error += "\n\r" + ex.Message;
                }
            }



            return result;

        }



        private class ReadResult
        {
            public string Error { get; set; }
            public Dictionary<string, List<string>> TimeInput { get; set; }
        }

    }
}
