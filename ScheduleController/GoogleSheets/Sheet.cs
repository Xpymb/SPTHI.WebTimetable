using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleController.GoogleSheets
{
    public class Sheet
    {
        public string Name { get; private set; }
        public string SheetId { get; private set; }
        public IReadOnlyList<string> ListNames { get => _listNames.AsReadOnly(); }

        private List<string> _listNames;

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetSheetId(string id)
        {
            SheetId = id;
        }

        public void SetListNames(List<string> listNames)
        {
            _listNames = listNames;
        }
    }
}
