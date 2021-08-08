using ScheduleController.GoogleSheets;
using ScheduleController.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using ScheduleController.Schedule.Model;

namespace ScheduleController.Schedule
{
    public static class ScheduleManager
    {
        private static readonly List<Group> _groups = new();
        private static readonly List<GroupType> _groupsType = new();
        
        public static void UpdateSchedule(object sender, EventArgs e)
        {
            var sheets = SheetsAPI.GetSheets();

            _groups.Clear();
            _groupsType.Clear();

            foreach(var sheet in sheets)
            {
                foreach(var listName in sheet.ListNames)
                {
                    var range = SheetsAPI.CreateRangeValues(listName, SheetsAPI.Range);

                    var schedule = SheetsAPI.GetSheetValues(sheet.SheetId, range);

                    if (schedule == null || schedule.Count <= 0)
                    {
                        continue;
                    }

                    var lessons = GetLessons(schedule);

                    foreach (var lesson in lessons)
                    {
                        lesson.WeekType = listName;

                        if (_groups.Count == 0)
                        {
                            AddGroup(lesson.GroupName, sheet.Name, listName, lesson.Class);
                        }

                        for (var i = 0; i <= _groups.Count; i++)
                        {
                            if (i == _groups.Count)
                            {
                                AddGroup(lesson.GroupName, sheet.Name, listName, lesson.Class);
                                _groups[i].Lessons.Add(lesson);

                                break;
                            }
                            else if (lesson.GroupName == _groups[i].Name && lesson.WeekType == _groups[i].WeekType && _groups[i].GroupTypeContains(GroupType.GetGroupTypeByString(sheet.Name)))
                            {
                                _groups[i].Lessons.Add(lesson);

                                break;
                            }
                        }
                    }
                }
            }

            UpdateListClasses();
        }

        private static void UpdateListClasses()
        {
            foreach (var groupType in _groupsType)
            {
                groupType.UpdateClassList();
            }
        }

        private static void AddGroup(string groupName, string groupTypeString, string weekType, string numClass)
        {
            var groupType = GroupType.GetGroupTypeByString(groupTypeString);
            var _class = new Class() { Name = numClass, GroupType = groupType, };

            var group = new Group
            {
                Name = groupName,
                Class = _class,
                Lessons = new List<Lesson>(),
                GroupType = groupType,
                WeekType = weekType,
            };

            _groups.Add(group);

            AddGroupType(groupType, group);
        }

        private static void AddGroupType(GroupType.Type type, Group group)
        {
            if(GroupType.ListContains(_groupsType, type))
            {
                GroupType.AddToList(_groupsType, type, group);

                return;
            }

            var groupType = new GroupType()
            {
                Name = type,
                Groups = new List<Group>(),
                Classes = new List<Class>(),
            };

            groupType.Groups.Add(group);

            _groupsType.Add(groupType);
        }

        public static List<Lesson> GetSchedule(string groupName)
        {
            foreach(var group in _groups)
            {
                if(group.Name == groupName)
                {
                    return group.Lessons;
                }
            }

            return null;
        }

        public static List<Lesson> GetSchedule(string groupName, GroupType groupType, string weekType)
        {
            foreach (var group in groupType.Groups)
            {
                if (group.Name == groupName && group.WeekType == weekType)
                {
                    return group.Lessons;
                }
            }

            return null;
        }

        public static List<Lesson> GetSchedule(string groupName, DateTime date)
        {
            foreach (var group in _groups)
            {
                if (group.Name == groupName)
                {
                    return GetLessonsByDate(group.Lessons, date);
                }
            }

            return null;
        }

        public static List<Lesson> GetSchedule(string groupName, DateTime date, string weekType)
        {
            foreach (var group in _groups)
            {
                if (group.Name == groupName && group.WeekType == weekType)
                {
                    return GetLessonsByDate(group.Lessons, date);
                }
            }

            return null;
        }

        public static List<string> GetClasses(string groupTypeString)
        {
            var groupType = GroupType.GetGroupType(_groupsType, GroupType.GetGroupTypeByString(groupTypeString));
            var classNameList = new List<string>();

            foreach(var _class in groupType.Classes)
            {
                classNameList.Add(_class.Name);
            }

            ArrayTools.BubbleSort(classNameList);

            return classNameList;
        }

        public static List<string> GetGroupTypes()
        {
            var groupTypesList = new List<string>();

            foreach(var groupType in _groupsType)
            {
                var groupTypeString = GroupType.GetStringByGroupType(groupType.Name);

                if(!groupTypesList.Contains(groupTypeString))
                {
                    groupTypesList.Add(groupTypeString);
                }
            }

            return groupTypesList;
        }

        public static List<string> GetWeeksType(string groupName, string groupTypeString)
        {
            var groupType = GroupType.GetGroupType(_groupsType, GroupType.GetGroupTypeByString(groupTypeString));
            var weeksTypeList = new List<string>();

            foreach(var group in groupType.Groups)
            {
                if(group.Name == groupName)
                {
                    weeksTypeList.Add(group.WeekType);
                }
            }

            return weeksTypeList;
        }

        public static List<string> GetGroupsName()
        {
            var listGroupsName = new List<string>();

            foreach (var group in _groups)
            {
                listGroupsName.Add(group.Name);
            }

            return listGroupsName;
        }

        public static List<string> GetGroupsName(string _class, string groupTypeString)
        {
            var groupType = GroupType.GetGroupType(_groupsType, GroupType.GetGroupTypeByString(groupTypeString));
            var listGroupsName = new List<string>();

            foreach (var group in groupType.Groups)
            {
                if(group.Class.Name == _class && !listGroupsName.Contains(group.Name))
                {
                    listGroupsName.Add(group.Name);
                }
            }

            return listGroupsName;
        }

        public static List<DateTime> GetDatesLessons(string groupName, string groupTypeString, string weekType)
        {
            var groupType = GroupType.GetGroupType(_groupsType, GroupType.GetGroupTypeByString(groupTypeString));

            var dates = new List<DateTime>();

            var lessons = GetSchedule(groupName, groupType, weekType);

            var prevDate = new DateTime();

            foreach (var lesson in lessons)
            {
                if (lesson.DateTime.Date != prevDate.Date)
                {
                    var date = lesson.DateTime.Date;

                    prevDate = date;

                    dates.Add(date);
                }
            }

            return dates;
        }

        public static void SubscribeOnUpdates()
        {
            Ticks.TicksController.TickEvery5Min += UpdateSchedule;
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

                var time = lesson[1].ToString().Split("-")[0];
                var _class = lesson[7].ToString().Remove(1, lesson[7].ToString().Length - 1);
                var groupName =$"{lesson[6]}-{_class + (((DateTime.Today.Year % 100) - Convert.ToInt32(_class)) % 10).ToString()}{lesson[7].ToString().Remove(0, 1)}";

                lessons.Add(new Lesson
                {
                    DateTime = DateTime.ParseExact($"{date} {time}", "dd.MM.yyyy H:mm", CultureInfo.InvariantCulture),
                    Type = lesson[2].ToString(),
                    Name = lesson[3].ToString(),
                    Classroom = lesson[4].ToString(),
                    TeacherName = lesson[5].ToString(),
                    GroupName = groupName,
                    Class = lesson[7].ToString(),
                });
            }

            return lessons;
        }
    }
}
