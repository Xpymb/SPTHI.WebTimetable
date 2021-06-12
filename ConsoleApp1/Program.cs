
using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using OfficeOpenXml;
using ConsoleApp1.VkKeyboard;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(VkKeyboard.VkKeyboard.CreateKeyaboard(false, new string[] { "Привет!", "Пока!" }, new VkKeyboard.VkKeyboard.ButtonColor[] { VkKeyboard.VkKeyboard.ButtonColor.White, VkKeyboard.VkKeyboard.ButtonColor.Blue }));

            

            /*ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileStream("schedule.xlsx", FileMode.Open, FileAccess.Read)))
            {
                var lessons = package.Workbook.Worksheets[14].Cells["S6:S21"].Value;

                for(int i = 0; )

                foreach(var lesson in lessons)
                {
                    Console.WriteLine(lesson);
                }
            }*/

            Console.ReadKey();
        }
    }
}
