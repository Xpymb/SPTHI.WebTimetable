using ScheduleController.Tools;
using System;
using System.Collections.Generic;

namespace ScheduleController.Schedule.Model
{
    public class GroupType
    {
        public enum Type
        {
            College,
            Institute,
            Magistracy,
            Aspiranture,
        }

        public Type Name { get; set; }
        public List<Group> Groups { get; set; }
        public List<Class> Classes { get; set; }

        public static Type GetGroupTypeByString(string groupTypeString)
        {
            var groupType = Type.College;

            switch (groupTypeString)
            {
                case "Колледж":
                    groupType = Type.College;
                    break;

                case "Институт":
                    groupType = Type.Institute;
                    break;

                case "Магистратура":
                    groupType = Type.Magistracy;
                    break;

                case "Аспирантура":
                    groupType = Type.Aspiranture;
                    break;
            }

            return groupType;
        }

        public void UpdateClassList()
        {
            Classes.Clear();

            foreach(var group in Groups)
            {
                if(Classes.Count == 0)
                {
                    Classes.Add(group.Class);

                    continue;
                }

                if(!ClassesListContains(Classes, group.Class.Name))
                {
                    Classes.Add(group.Class);
                }
            }
        }

        public static GroupType GetGroupType(List<GroupType> groupsType, Type type)
        {
            foreach(var groupType in groupsType)
            {
                if(groupType.Name == type)
                {
                    return groupType;
                }
            }

            return null;
        }

        public static string GetStringByGroupType(GroupType.Type groupType)
        {
            string groupTypeString = groupType switch
            {
                Type.College => "Колледж",
                Type.Institute => "Институт",
                Type.Magistracy => "Магистратура",
                Type.Aspiranture => "Аспирантура",
                _ => "",
            };
            return groupTypeString;
        }

        public static bool ListContains(List<GroupType> groupsType, Type type)
        {
            foreach(var groupType in groupsType)
            {
                if(groupType.Name == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static void AddToList(List<GroupType> groupsType, Type type, Group group)
        {
            foreach(var groupType in groupsType)
            {
                if(groupType.Name == type)
                {
                    groupType.Groups.Add(group);

                    return;
                }
            }
        }

        private static bool ClassesListContains(List<Class> classes, string className)
        {
            foreach(var _class in classes)
            {
                if(_class.Name == className)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
