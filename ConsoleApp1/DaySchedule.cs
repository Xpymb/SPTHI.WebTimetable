using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class DaySchedule
    {
        private List<string> _lessons;

        public IReadOnlyList<string> Lessons { get => _lessons.AsReadOnly(); }

        public DaySchedule()
        {

        }
    }
}
