using System.Collections.Generic;

namespace ScheduleController.Schedule.Model
{
    public class Group
    {
        public string Name { get; set; }
        public Class Class { get; set; }
        public GroupType.Type GroupType { get; set; }
        public List<Lesson> Lessons { get; set; }
        public string WeekType { get; set; }

        public bool GroupTypeContains(GroupType.Type groupType)
        {
            return GroupType == groupType;
        }
    }
}
