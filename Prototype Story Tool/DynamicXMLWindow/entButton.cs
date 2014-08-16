using System;
using System.Collections.Generic;

namespace DynamicXMLWindow
{
    class entButton
    {
        public string text {get; set;}
        public List<int> btnIndex { get; set; } // OLD Remove once it's no longer called
        public string sceneIndex { get; set; }
        public List<Condition> conditions { get; set; }
        public string action { get; set; }
        public string result { get; set; }
        public bool visible { get; set; }
    }
}
