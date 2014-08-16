using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicXMLWindow
{
    class DialogWinStruct
    {
        public string dialog { get; set; }
        public List<entItem> items { get; set; }
        public List<entButton> buttons { get; set; }
        public List<Condition> conditions { get; set; }
    }
}
