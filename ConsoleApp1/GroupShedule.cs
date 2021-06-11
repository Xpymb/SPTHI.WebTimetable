namespace ConsoleApp1
{
    public class GroupShedule
    {
        public string GroupName { get; }
        public string NameOfColumn { get; }

        public GroupShedule(string groupName, string nameOfColumn)
        {
            GroupName = groupName;
            NameOfColumn = nameOfColumn;
        }
    }
}
