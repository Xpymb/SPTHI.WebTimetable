using System.Collections.Generic;

namespace ScheduleController.Schedule
{
    public class Group
    {
        public string Name { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
