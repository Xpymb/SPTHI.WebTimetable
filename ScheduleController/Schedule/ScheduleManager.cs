using ScheduleController.GoogleSheets;
using System;
using System.Collections.Generic;

namespace ScheduleController.Schedule
{
    public static class ScheduleManager
    {
        private static readonly List<string> _groupsName = new() 
        { 
            "МП-21д",
            "МП-21дп",
            "ТМО-21д",
            "ТМО-21дп",
            "ДО-21д"
        };

        public static List<Group> Groups { get; private set; }

        public static void UpdateSchedule()
        {
            var schedule = SheetsAPI.GetSheetValues(SheetsAPI.SpreadSheetId, SheetsAPI.RangeValues);

            if(schedule != null && schedule.Count > 0)
            {
                Groups = CreateGroupsList(_groupsName);

                var lessons = GetLessons(schedule);

                foreach(var group in Groups)
                {
                    foreach(var lesson in lessons)
                    {
                        if(lesson.GroupName == group.Name)
                        {
                            group.Lessons.Add(lesson);
                        }
                    }
                }
            }
        }

        public static List<Lesson> GetScheduleByGroupName(string groupName)
        {
            foreach(var group in Groups)
            {
                if(groupName == group.Name)
                {
                    return group.Lessons;
                }
            }

            return null;
        }

        public static List<Lesson> GetScheduleByGroupName(string groupName, DateTime date)
        {
            foreach (var group in Groups)
            {
                if (groupName == group.Name)
                {
                    return GetLessonsByDate(group.Lessons, date);
                }
            }

            return null;
        }

        public static List<string> GetGroupsName()
        {
            return _groupsName;
        }

        public static List<DateTime> GetDatesLessonsByGroup(string groupName)
        {
            var dates = new List<DateTime>();

            var lessons = GetScheduleByGroupName(groupName);

            var prevDate = new DateTime();

            foreach (var lesson in lessons)
            {
                if (lesson.DateTime != prevDate)
                {
                    dates.Add(lesson.DateTime);
                }
            }

            return dates;
        }

        private static List<Lesson> GetLessonsByDate(List<Lesson> lessons, DateTime date)
        {
            var listLessons = new List<Lesson>();

            foreach(var lesson in lessons)
            {
                if(lesson.DateTime.Date == date.Date)
                {
                    listLessons.Add(lesson);
                }
            }

            return listLessons;
        }

        private static List<Group> CreateGroupsList(List<string> groupsName)
        {
            var groups = new List<Group>();

            foreach (var groupName in groupsName)
            {
                groups.Add(new Group { Name = groupName, Lessons = new List<Lesson>() });
            }

            return groups;
        }

        private static List<Lesson> GetLessons(IList<IList<object>> objects)
        {
            var lessons = new List<Lesson>();

            var date = "";

            foreach (var lesson in objects)
            {
                if (lesson[0].ToString() != "")
                {
                    date = lesson[0].ToString();
                }

                lessons.Add(new Lesson
                {
                    DateTime = DateTime.Parse($"{date} {lesson[1].ToString().Split("-")[0]}"),
                    Type = lesson[2].ToString(),
                    Name = lesson[3].ToString(),
                    Classroom = lesson[4].ToString(),
                    TeacherName = lesson[5].ToString(),
                    GroupName = lesson[6].ToString(),
                    Class = lesson[7].ToString(),
                });
            }

            return lessons;
        }
    }
}
