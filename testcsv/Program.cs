﻿using System;
using System.Diagnostics;
using System.IO;

namespace testcsv
{
    public class LocalWeatherData
    {
        public string WBAN;
        public DateTime Date;
        public string SkyCondition;
    }

    public class cars
    {
        public int Year;
        public string Make;
        public string Model;
        public string Description;
        public decimal Price;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            if (File.Exists("..\\..\\..\\csvstandard.csv"))
            {
                var listcars = fastCSV.ReadFile<cars>("..\\..\\..\\csvstandard.csv", true, ',', (o, c) =>
                   {
                       o.Year = fastCSV.ToInt(c[0]);
                       o.Make = c[1];
                       o.Model = c[2];
                       o.Description = c[3];
                       o.Price = decimal.Parse(c[4]);
                       return true;
                   });
            }
            var line = 1;
            if (File.Exists("d:/201503hourly.txt") == false)
            {
                Console.WriteLine("Please download 201503hourly.txt from : https://www.ncdc.noaa.gov/orders/qclcd/QCLCD201503.zip");
                Console.WriteLine("press any key.");
                Console.ReadKey();
                return;
            }

            sw.Start();
            var list = fastCSV.ReadFile<LocalWeatherData>("d:/201503hourly.txt", true, ',', (o, c) =>
                {
                    bool add = true;
                    line++;
                    o.WBAN = c[0];
                    o.Date = new DateTime(fastCSV.ToInt(c[1], 0, 4),
                                          fastCSV.ToInt(c[1], 4, 2),
                                          fastCSV.ToInt(c[1], 6, 2));
                    o.SkyCondition = c[4];
                    //if (o.Date.Day % 2 == 0)
                    //    add = false;
                    return add;
                });
            sw.Stop();
            Console.WriteLine("read " + line + " time : " + sw.Elapsed.TotalSeconds + " sec");
            //GC.Collect();
            //GC.Collect(2);

            sw.Restart();
            // specific items in list
            //fastCSV.WriteFile<LocalWeatherData>("filename2.csv", new string[] { "WBAN", "Date", "SkyCondition" }, '|', list, (o, c) =>
            //{
            //    c.Add(o.WBAN);
            //    c.Add(o.Date.ToString("yyyyMMdd"));
            //    c.Add(o.SkyCondition);
            //});
            //Console.WriteLine("write time : " + sw.Elapsed.TotalSeconds + " sec");
            Console.WriteLine("press any key.");
            Console.ReadKey();
        }
    }
}



