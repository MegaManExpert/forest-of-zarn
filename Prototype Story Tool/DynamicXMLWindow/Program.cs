using System;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections;

namespace DynamicXMLWindow
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static List<DialogWinStruct> winList = new List<DialogWinStruct>(); // OLD Remove once moved to new Hashtable
        private static Dictionary<string, dynamic> testCase = new Dictionary<string, dynamic>(); // OLD Remove later

        public static Hashtable storyMap = new Hashtable();
        public static Hashtable storyItemMap = new Hashtable();
        public static Player player = new Player()
        {
            items = new List<string>()
        };

        [STAThread]
        static void Main()
        {
            itemXML();
            readThisXML();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new XMLDialog());           
        }

        private static void itemXML()
        {
            XDocument doc = XDocument.Load("c:\\users\\mmichalske\\documents\\visual studio 2012\\Projects\\DynamicXMLWindow\\DynamicXMLWindow\\StoryItems.xml");

            foreach (XElement root in doc.Root.Elements("item"))
            {
                entItem item = new entItem();
                int num;

                item.key = (string)root.Element("key").Value;
                item.name = (string)root.Element("name").Value;
                item.discription = (string)root.Element("discription").Value;
                item.type = (string)root.Element("type").Value;
                item.equipment = (string)root.Element("equipment").Value;
                if (int.TryParse((string)root.Element("uses").Value, out num))
                    item.uses = Convert.ToInt32((string)root.Element("uses").Value);
                else
                    item.uses = 0;
                item.taken = false;

                storyItemMap.Add((string)root.Element("key").Value, item);
            }
        }
        private static void readThisXML()
        {
            XDocument doc = XDocument.Load("c:\\users\\mmichalske\\documents\\visual studio 2012\\Projects\\DynamicXMLWindow\\DynamicXMLWindow\\NewStoryTest.xml");

            //Player Testing!!!!
            //List<string> testItems = new List<string>();
            //testItems.Add("Light");
            //player.items = testItems;

            List<entItem> items = new List<entItem>();
            List<entButton> buttons = new List<entButton>();
            List<Condition> conditions = new List<Condition>();

            foreach (XElement root in doc.Root.Elements("window"))
            {                
                DialogWinStruct dwStruct = new DialogWinStruct();
                items = new List<entItem>();
                buttons = new List<entButton>();
                conditions = new List<Condition>();

                foreach (XElement element in root.Elements("item"))
                {
                    string key = (string)element.Attribute("itemKey");
                    items.Add((entItem)storyItemMap[key]);
                }

                foreach (XElement element in root.Elements("button"))
                {
                    entButton btn = new entButton();
                    List<Condition> btnConditions = new List<Condition>();

                    btn.text = (string)element.Attribute("text");
                    btn.sceneIndex = (string)element.Attribute("winScene");
                    btn.visible = (bool)element.Attribute("visible");

                    if (element.Attribute("action").Value.Contains("getItem"))
                    {
                        btn.action = (string)element.Attribute("action");
                        btn.result = (string)element.Attribute("result");
                    }
                    if (element.Attribute("action").Value.Contains("useItem"))
                    {
                        btn.action = (string)element.Attribute("action");
                        btn.result = (string)element.Attribute("result");
                    }

                    foreach (XElement elementC in element.Elements("condition"))
                    {
                        if (elementC.Attribute("type").Value.Equals("hasItem"))
                        {
                            Condition con = new HasItemCondition((string)elementC.Attribute("itemType"), (string)elementC.Element("fail").Value, (string)elementC.Element("success").Value, (bool)elementC.Attribute("met"));
                            btnConditions.Add(con);
                        }
                        if (elementC.Attribute("type").Value.Equals("itemEquipHide"))
                        {
                            Condition con = new HideItemHasCondition((string)elementC.Attribute("itemType"), (string)elementC.Element("message").Value);
                            btnConditions.Add(con);
                        }
                        if (elementC.Attribute("type").Value.Equals("useOnce"))
                        {
                            Condition con = new UseOnceCondition(false, (string)elementC.Element("message").Value);
                            btnConditions.Add(con);
                        }
                    }
                    btn.conditions = btnConditions;

                    buttons.Add(btn);
                }

                dwStruct.buttons = buttons;

                foreach (XElement element in root.Elements("dialog"))
                {
                    dwStruct.dialog = (string)element.Value;

                    System.Diagnostics.Debug.WriteLine("Dialog: " + (string)element.Value);
                }

                foreach (XElement element in root.Elements("condition"))
                {
                    if (element.Attribute("type").Value.Equals("hasItem"))
                    {
                        Condition con = new HasItemCondition((string)element.Attribute("itemType"), (string)element.Element("fail").Value, (string)element.Element("success").Value, (bool)element.Attribute("met"));
                        conditions.Add(con);
                    }
                    if (element.Attribute("type").Value.Equals("itemEquipHide"))
                    {
                        Condition con = new HideItemHasCondition((string)element.Attribute("itemType"), (string)element.Element("message").Value);
                        conditions.Add(con);
                    }
                }

                dwStruct.conditions = conditions;

                dwStruct.items = items;

                storyMap.Add((string)root.Attribute("scene"),dwStruct);
            }
        }

        //private static void readThisXML2()
        //{
        //    XDocument doc = XDocument.Load("c:\\users\\mmichalske\\documents\\visual studio 2012\\Projects\\DynamicXMLWindow\\DynamicXMLWindow\\SampleStory.xml");

        //    //Player Testing!!!!
        //    //List<string> testItems = new List<string>();
        //    //testItems.Add("Light");
        //    //player.items = testItems;

        //    List<entButton> buttons = new List<entButton>();
        //    List<Condition> conditions = new List<Condition>();

        //    foreach (XElement root in doc.Root.Elements("window"))
        //    {
        //        DialogWinStruct dwStruct = new DialogWinStruct();
        //        buttons = new List<entButton>();
        //        conditions = new List<Condition>();

        //        foreach (XElement element in root.Elements("button"))
        //        {
        //            entButton btn = new entButton();
        //            List<int> btnIndex = new List<int>();
        //            List<Condition> btnConditions = new List<Condition>();

        //            btn.text = (string)element.Attribute("text");
        //            btn.action = (string)element.Attribute("text");
        //            btnIndex.Add((int)element.Attribute("winIndex"));
        //            btn.btnIndex = btnIndex;

        //            if (element.Attribute("action").Value.Equals("getItem"))
        //            {
        //                btn.action = (string)element.Attribute("action");
        //                btn.result = (string)element.Attribute("result");
        //            }

        //            foreach (XElement elementC in element.Elements("condition"))
        //            {
        //                if (elementC.Attribute("type").Value.Equals("itemEquip"))
        //                {
        //                    Condition con = new ItemHasCondition((string)elementC.Attribute("itemType"), (string)elementC.Element("message").Value);
        //                    btnConditions.Add(con);
        //                }
        //                if (elementC.Attribute("type").Value.Equals("itemEquipHide"))
        //                {
        //                    Condition con = new HideItemHasCondition((string)elementC.Attribute("itemType"), (string)elementC.Element("message").Value);
        //                    btnConditions.Add(con);
        //                }
        //                if (elementC.Attribute("type").Value.Equals("useOnce"))
        //                {
        //                    Condition con = new UseOnceCondition(false, (string)elementC.Element("message").Value);
        //                    btnConditions.Add(con);
        //                }
        //            }
        //            btn.conditions = btnConditions;

        //            buttons.Add(btn);

        //            System.Diagnostics.Debug.WriteLine("Text: {0}; Window Index: {1}",
        //                              (string)element.Attribute("text"),
        //                              (string)element.Attribute("winIndex"));
        //        }

        //        dwStruct.buttons = buttons;

        //        foreach (XElement element in root.Elements("dialog"))
        //        {
        //            dwStruct.dialog = (string)element.Value;

        //            System.Diagnostics.Debug.WriteLine("Dialog: " + (string)element.Value);
        //        }

        //        foreach (XElement element in root.Elements("condition"))
        //        {
        //            if (element.Attribute("type").Value.Equals("itemEquip"))
        //            {
        //                Condition con = new ItemHasCondition((string)element.Attribute("itemType"), (string)element.Element("message").Value);
        //                conditions.Add(con);
        //            }
        //            if (element.Attribute("type").Value.Equals("itemEquipHide"))
        //            {
        //                Condition con = new HideItemHasCondition((string)element.Attribute("itemType"), (string)element.Element("message").Value);
        //                conditions.Add(con);
        //            }
        //        }

        //        dwStruct.conditions = conditions;

        //        winList.Add(dwStruct);
        //    }
        //}

        //private static void readThisXML1()
        //{
        //    XDocument doc = XDocument.Load("c:\\users\\mmichalske\\documents\\visual studio 2012\\Projects\\DynamicXMLWindow\\DynamicXMLWindow\\XMLDialog.xml");

        //    List<entButton> buttons = new List<entButton>();
        //    List<Condition> conditions = new List<Condition>();

        //    foreach (XElement root in doc.Root.Elements("window"))
        //    {
        //        DialogWinStruct dwStruct = new DialogWinStruct();
        //        buttons = new List<entButton>();
        //        conditions = new List<Condition>();

        //        foreach (XElement element in root.Elements("button"))
        //        {
        //            entButton btn = new entButton();
        //            List<int> btnIndex = new List<int>();
        //            List<Condition> btnConditions = new List<Condition>();

        //            btn.text = (string)element.Attribute("text");
        //            btn.action = (string)element.Attribute("text");
        //            btnIndex.Add((int)element.Attribute("winIndex"));
        //            btn.btnIndex = btnIndex;

        //            if (element.Attribute("action").Value.Equals("getItem"))
        //            {
        //                btn.action = (string)element.Attribute("action");
        //                btn.result = (string)element.Attribute("result");
        //            }

        //            foreach (XElement elementC in element.Elements("condition"))
        //            {
        //                if (elementC.Attribute("type").Value.Equals("itemEquip"))
        //                {
        //                    Condition con = new ItemHasCondition((string)elementC.Attribute("itemType"), (string)elementC.Element("message").Value);
        //                    btnConditions.Add(con);
        //                }
        //                if (elementC.Attribute("type").Value.Equals("itemEquipHide"))
        //                {
        //                    Condition con = new HideItemHasCondition((string)elementC.Attribute("itemType"), (string)elementC.Element("message").Value);
        //                    btnConditions.Add(con);
        //                }
        //            }
        //            btn.conditions = btnConditions;

        //            buttons.Add(btn);

        //            System.Diagnostics.Debug.WriteLine("Text: {0}; Window Index: {1}",
        //                              (string)element.Attribute("text"),
        //                              (string)element.Attribute("winIndex"));
        //        }

        //        dwStruct.buttons = buttons;

        //        foreach (XElement element in root.Elements("dialog"))
        //        {
        //            dwStruct.dialog = (string)element.Value;

        //            System.Diagnostics.Debug.WriteLine("Dialog: " + (string)element.Value);
        //        }

        //        foreach (XElement element in root.Elements("condition"))
        //        {
        //            if (element.Attribute("type").Value.Equals("itemEquip"))
        //            {
        //                Condition con = new ItemHasCondition((string)element.Attribute("itemType"), (string)element.Element("message").Value);
        //                conditions.Add(con);
        //            }
        //            if (element.Attribute("type").Value.Equals("itemEquipHide"))
        //            {
        //                Condition con = new HideItemHasCondition((string)element.Attribute("itemType"), (string)element.Element("message").Value);
        //                conditions.Add(con);
        //            }
        //        }

        //        dwStruct.conditions = conditions;

        //        winList.Add(dwStruct);

        //    }

        //    testCase.Add("buttons", buttons);
        //    testCase.Add("conditions", conditions);
        //}
    }
}
