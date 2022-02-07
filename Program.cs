using System;
using System.IO;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 1 to create file data");
            Console.WriteLine("Press 2 to parse data");
            Console.WriteLine("Press any other key to quit");

            string resp = Console.ReadLine();
            var file = "data.txt";

            if (resp == "1") {
                Console.WriteLine("How many weeks of data is required?");

                int weeks = int.Parse(Console.ReadLine());

                DateTime today = DateTime.Now;
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                Random rnd = new Random();
                StreamWriter sw = new StreamWriter(file);

                while (dataDate < dataEndDate) {
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++) {
                        hours[i] = rnd.Next(4, 13);
                    }

                    sw.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2") {

                if (File.Exists(file)) {
                    StreamReader sr = new StreamReader(file);
                    while (!sr.EndOfStream) {
                        string line = sr.ReadLine();

                        string[] week = line.Split(',');
                        DateTime date = DateTime.Parse(week[0]);

                        int[] hours = Array.ConvertAll(week[1].Split('|'), int.Parse);

                        int total = 0;
                        foreach(int hour in hours) {
                            total+= hour;
                        }
                        double avg = (double)total / 7;    

                        Console.WriteLine($"Week of {date:MMM}, {date:dd}, {date:yyyy}");
                        Console.WriteLine($"{"Su",3}{"Mo",3}{"Tu",3}{"We",3}{"Th",3}{"Fr",3}{"Sa",3}{"Tot",4}{"Avg",4}");
                        Console.WriteLine($"{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"---",4}{"---",4}");
                        Console.WriteLine($"{hours[0],3}{hours[1],3}{hours[2],3}{hours[3],3}{hours[4],3}{hours[5],3}{hours[6],3}{total,4}{avg.ToString("F1"),4}");
                        Console.WriteLine();
                    }
                } else Console.WriteLine("File does not exist");
            }
        }
    }
}
