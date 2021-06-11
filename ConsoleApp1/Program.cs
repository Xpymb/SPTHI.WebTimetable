
using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using OfficeOpenXml;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileStream("schedule.xlsx", FileMode.Open, FileAccess.Read)))
            {
                var lessons = package.Workbook.Worksheets[14].Cells["S6:S21"].Value;

                for(int i = 0; )

                foreach(var lesson in lessons)
                {
                    Console.WriteLine(lesson);
                }
            }

            Console.ReadKey();
        }
    }
}
