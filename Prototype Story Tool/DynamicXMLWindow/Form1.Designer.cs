using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DynamicXMLWindow
{
    partial class XMLDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private List<int> nextBtnIndex;
        private List<string> nextBtnScene;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.DialogBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(94, 241);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(176, 241);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(258, 241);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(340, 241);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // DialogBox
            // 
            this.DialogBox.Location = new System.Drawing.Point(13, 13);
            this.DialogBox.Multiline = true;
            this.DialogBox.Name = "DialogBox";
            this.DialogBox.ReadOnly = true;
            this.DialogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DialogBox.Size = new System.Drawing.Size(404, 222);
            this.DialogBox.TabIndex = 5;
            // 
            // XMLDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 277);
            this.Controls.Add(this.DialogBox);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "XMLDialog";
            this.Text = "XML Dialog";
            this.Load += new System.EventHandler(this.Form1_BtnStart);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Button actions for the form
        /// <summary>
        /// Please keep to only the number of buttons for the project
        /// </summary>

        //  OLD Current window the button is tied to
        // --- OLD --
        int currentIndex = 0; //<-- OLD Not used anymore

        // Current window the button is tied to
        // from the key (Name of scene)
        string currentKey = "start";

        private void Button1_Click(System.Object sender, System.EventArgs e)
        {
            // --- OLD STYLE ---
            //if (Program.winList[currentIndex].buttons.Count > 0)
            //    Form1_BtnAction(currentIndex, 0);
            //currentIndex = Form1_BtnMap(nextBtnIndex[0]);

            Form1_BtnAction(currentKey, 0);
            currentKey = Form1_BtnMap(nextBtnScene[0]);
        }

        private void Button2_Click(System.Object sender, System.EventArgs e)
        {
            Form1_BtnAction(currentKey, 1);
            currentKey = Form1_BtnMap(nextBtnScene[1]);
        }

        private void Button3_Click(System.Object sender, System.EventArgs e)
        {
            Form1_BtnAction(currentKey, 2);
            currentKey = Form1_BtnMap(nextBtnScene[2]);            
        }

        private void Button4_Click(System.Object sender, System.EventArgs e)
        {
            Form1_BtnAction(currentKey, 3);
            currentKey = Form1_BtnMap(nextBtnScene[3]);            
        }

        private void Button5_Click(System.Object sender, System.EventArgs e)
        {
            Form1_BtnAction(currentKey, 4);
            currentKey = Form1_BtnMap(nextBtnScene[4]);            
        }
        #endregion

        #region OLD Button map for form
        // Loads the XML into memory from the parsed list
        //private int Form1_BtnMapOLD(int currentWindow)
        //{
        //    button1.Visible = false;
        //    button2.Visible = false;
        //    button3.Visible = false;
        //    button4.Visible = false;
        //    button5.Visible = false;

        //    DialogBox.Text = Program.winList[currentWindow].dialog;
        //    AreAllConditionsMetForWindow(currentWindow);

        //    nextBtnIndex = new List<int>();
        //    foreach (entButton b in Program.winList[currentWindow].buttons)
        //    {
        //        nextBtnIndex.Add(b.btnIndex[0]);
        //    }

        //    for (int btnIndex = 0; btnIndex < Program.winList[currentWindow].buttons.Count; btnIndex++)
        //    {
        //        switch (btnIndex)
        //        {
        //            case 0:
        //                button1.Visible = AreAllConditionsMet(currentWindow, btnIndex);
        //                button1.Text = Program.winList[currentWindow].buttons[btnIndex].text;
        //                break;
        //            case 1:
        //                button2.Visible = AreAllConditionsMet(currentWindow, btnIndex);
        //                button2.Text = Program.winList[currentWindow].buttons[btnIndex].text;
        //                break;
        //            case 2:
        //                button3.Visible = AreAllConditionsMet(currentWindow, btnIndex);
        //                button3.Text = Program.winList[currentWindow].buttons[btnIndex].text;
        //                break;
        //            case 3:
        //                button4.Visible = AreAllConditionsMet(currentWindow, btnIndex);
        //                button4.Text = Program.winList[currentWindow].buttons[btnIndex].text;
        //                break;
        //            case 4:
        //                button5.Visible = AreAllConditionsMet(currentWindow, btnIndex);
        //                button5.Text = Program.winList[currentWindow].buttons[btnIndex].text;
        //                break;
        //            default:
        //                button1.Visible = false;
        //                button2.Visible = false;
        //                button3.Visible = false;
        //                button4.Visible = false;
        //                button5.Visible = false;
        //                break;
        //        }
        //    }

        //    return currentWindow;
        //}
         #endregion

        private string Form1_BtnMap(string sceneKey)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;

            DialogWinStruct scene = (DialogWinStruct)Program.storyMap[sceneKey];
            DialogBox.Text = scene.dialog;

            // Tokenize the item tags as a list and is able to be pulled apart for later
            string itemList = "";
            foreach(entItem item in scene.items)
            {
                if (!item.taken)
                    itemList += " " + item.name + ",";
            }
            itemList = itemList.TrimEnd(',');
            DialogBox.Text = DialogBox.Text.Replace("@items", itemList);

            // Tokenize the items into there seprate parts for the current scene
            string itemObject = "";
            for (int itemIndex = 0; itemIndex < scene.items.Count; itemIndex++)
            {
                if (scene.items[itemIndex].taken)
                {
                    itemObject = "";
                }
                else
                {
                    itemObject = scene.items[itemIndex].name;
                }
                DialogBox.Text = DialogBox.Text.Replace("@item" + itemIndex, itemObject);
            }

            // Tokenize the conditions for the current scene for text that relate the order of conditions
            string responseMessage = "";
            for(int condIndex = 0; condIndex < scene.conditions.Count; condIndex++)
            {                
                if (scene.conditions[condIndex].IsThisConditionMet())
                {
                    responseMessage = scene.conditions[condIndex].successMessage;                        
                }
                else
                {
                    responseMessage = scene.conditions[condIndex].failMessage;
                }
                DialogBox.Text = DialogBox.Text.Replace("@condition" + condIndex, responseMessage);
            }

            //AreAllConditionsMetForWindow(sceneKey); <-- Old but might be useful later for actions

            // Find all the buttons and get all there key pointers
            nextBtnScene = new List<string>();
            foreach (entButton button in scene.buttons)
            {
                nextBtnScene.Add(button.sceneIndex);
            }

            for (int btnIndex = 0; btnIndex < scene.buttons.Count; btnIndex++)
            {
                switch (btnIndex)
                {
                    case 0:
                        //button1.Visible = scene.buttons[btnIndex].visible; <-- Old replaced with the below function
                        button1.Visible = AreAllConditionsMet(sceneKey, btnIndex);                        
                        button1.Text = scene.buttons[btnIndex].text;
                        break;
                    case 1:
                        button2.Visible = AreAllConditionsMet(sceneKey, btnIndex);
                        button2.Text = scene.buttons[btnIndex].text;
                        break;
                    case 2:
                        button3.Visible = AreAllConditionsMet(sceneKey, btnIndex);
                        button3.Text = scene.buttons[btnIndex].text;
                        break;
                    case 3:
                        button4.Visible = AreAllConditionsMet(sceneKey, btnIndex);
                        button4.Text = scene.buttons[btnIndex].text;
                        break;
                    case 4:
                        button5.Visible = AreAllConditionsMet(sceneKey, btnIndex);
                        button5.Text = scene.buttons[btnIndex].text;
                        break;
                    default:
                        button1.Visible = false;
                        button2.Visible = false;
                        button3.Visible = false;
                        button4.Visible = false;
                        button5.Visible = false;
                        break;
                }
            }

            return sceneKey;
        }

        public bool AreAllConditionsMet(string sceneKey, int btnIndex)
        {
            DialogWinStruct scene = (DialogWinStruct)Program.storyMap[sceneKey];
            bool result = scene.buttons[btnIndex].visible;
            string responseMessage = "";
            foreach (Condition condition in scene.buttons[btnIndex].conditions)
            {
                bool conResult = condition.IsThisConditionMet();
                if (conResult)
                {
                    responseMessage += condition.successMessage + "\n";
                }
                else
                {
                    
                    responseMessage += condition.failMessage + "\n";
                }
                DialogBox.Text += responseMessage;
                result &= conResult;

            }
            return result;
        }

        // Might need to update for hanging conditions that do more then add text
        public void AreAllConditionsMetForWindow(string sceneKey)
        {
            string responseMessage = "";
            DialogWinStruct scene = (DialogWinStruct)Program.storyMap[sceneKey];
            foreach (Condition condition in scene.conditions)
            {
                if (!condition.IsThisConditionMet())
                {
                    responseMessage += condition.failMessage + "\n";
                }
                else
                {
                    responseMessage += condition.successMessage + "\n";
                }
            }
            DialogBox.Text += responseMessage;
        }

        private void Form1_BtnAction(string sceneKey, int btnIndex)
        {
            DialogWinStruct scene = (DialogWinStruct)Program.storyMap[sceneKey];
            entItem item = new entItem();
            List<string> playerItems = new List<string>();
            string actionTrigger = scene.buttons[btnIndex].result;

            string[] btnAction = {""};
            if (scene.buttons[btnIndex].action != null)
            {
                if (scene.buttons[btnIndex].action.Contains(":"))
                {
                    btnAction = scene.buttons[btnIndex].action.Split(':');
                    actionTrigger = btnAction[1];
                }
                else
                    btnAction[0] = scene.buttons[btnIndex].action;
            }

            switch (btnAction[0])
            {
                case "getItem":
                    // Check to see if player has any items, if so get them
                    if (Program.player.items != null)
                        playerItems = Program.player.items;

                    // Add item to curent invatory
                    actionTrigger = btnAction[1];
                    playerItems.Add(actionTrigger);
                    Program.player.items = playerItems;

                    // Hide items from lists
                    scene.buttons[btnIndex].visible = false;
                    item = (entItem)Program.storyItemMap[actionTrigger];
                    item.taken = true;
                    Program.storyItemMap[actionTrigger] = item;
                    Form1_BtnMap(currentKey);
                    DialogBox.Text += "\r\n" + "You have taken the " + item.name + ".";
                    break;

                case "useItem":
                    if (Program.player.items != null)
                    {
                        playerItems = Program.player.items;
                        item = (entItem)Program.storyItemMap[actionTrigger];

                        if (playerItems.Contains(item.key) && item.uses > 0)
                        {
                            //playerItems.Remove(scene.buttons[btnIndex].result);
                            //Program.storyItemMap[actionTrigger] = item.uses - 1;
                            nextBtnScene[btnIndex] = scene.buttons[btnIndex].result;
                        }

                        Program.player.items = playerItems;
                    }
                    break;

                default:
                    break;
            }
            //if (scene.buttons[btnIndex].conditions.Count > 0)
            //{
            //    var test = scene.buttons[btnIndex].conditions;
            //    if (Program.player.items != null)
            //    {
            //        playerItems = Program.player.items;
            //        playerItems.Remove(scene.buttons[btnIndex].result);
            //        Program.player.items = playerItems;
            //    }
            //}
        }

        private void Form1_BtnActionOLD(int currentWindow, int btnIndex)
        {
            if (Program.winList[currentWindow].buttons[btnIndex].action.Equals("getItem"))
            {
                List<string> playerItems = new List<string>();
                if(Program.player.items != null)
                    playerItems = Program.player.items;
                playerItems.Add(Program.winList[currentWindow].buttons[btnIndex].result);
                Program.player.items = playerItems;

                //Program.winList[currentWindow].buttons[btnIndex].conditions[0].failMessage.Insert(0, "@");
            }
            if (Program.winList[currentWindow].buttons[btnIndex].action.Equals("useItem"))
            {
                List<string> playerItems = new List<string>();
                if (Program.player.items != null)
                {
                    playerItems = Program.player.items;
                    playerItems.Remove(Program.winList[currentWindow].buttons[btnIndex].result);
                    Program.player.items = playerItems;
                }
            }
            if (Program.winList[currentWindow].buttons[btnIndex].conditions.Count > 0 )
            {
                var test = Program.winList[currentWindow].buttons[btnIndex].conditions;
                List<string> playerItems = new List<string>();
                if (Program.player.items != null)
                {
                    playerItems = Program.player.items;
                    playerItems.Remove(Program.winList[currentWindow].buttons[btnIndex].result);
                    Program.player.items = playerItems;
                }
            }
        }

        private void Form1_BtnStart(System.Object sender, System.EventArgs e)
        {
            //
            // XML Setup at startup for first window
            //
            //Form1_BtnMap(0);
            Form1_BtnMap("start");
        }

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox DialogBox;
    }
}

