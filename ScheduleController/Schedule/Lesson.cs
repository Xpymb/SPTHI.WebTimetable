using System;

namespace ScheduleController.Schedule
{
    public class Lesson
    {
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Classroom { get; set; }
        public string TeacherName { get; set; }
        public string GroupName { get; set; }
        public string Class { get; set; }
    }
}
