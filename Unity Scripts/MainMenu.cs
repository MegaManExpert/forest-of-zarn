using UnityEngine;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainMenu : MonoBehaviour
{
	PlayerCreation playerCreationScript;
	MainMenu mainMenuScript;
	GameObject playerCam;
	
	// Game maps for story, items, and monsters
	static Hashtable storyMap = new Hashtable();
	static Hashtable storyItemMap = new Hashtable();
	static bool debugGame = false;
	
	Rect windowRect1 = new Rect(Screen.width/2 - 165, Screen.height/2 - 105, 330, 200);
	
	bool doWindow0 = false;
	bool doWindow1 = false;
	bool doWindow2 = false;

	public GUISkin StoneGuiSkin;
	
	void Start()
	{
		readItemXML();
		readStoryXML();
			
		playerCreationScript = GetComponent<PlayerCreation>();
		mainMenuScript = GetComponent<MainMenu>();
		playerCam = GameObject.FindGameObjectWithTag("Player");
		
		playerCam.active = false;
	}

	void OnGUI()
	{
		GUI.skin = StoneGuiSkin;

		// Make a group on the center of the screen
		GUI.BeginGroup (new Rect (5, Screen.height - 60, 410, 50));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

		// We'll make a box so you can see where the group is on-screen.
//		GUI.Box (new Rect (0,0,400,50), "Group is here");
		if(GUI.Button (new Rect (0,10,100,30), "Exit the game"))
			 Application.Quit();
		if(GUI.Button (new Rect (105,10,125,30), "Start New Game"))
			doWindow0 = true;
		if(GUI.Button (new Rect (235,10,85,30), "Load Game"))
			doWindow1 = true;
		if(GUI.Button (new Rect (325,10,85,30), "Debug Mode"))
			doWindow2 = true;

		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();
		
		if(doWindow0)
		{
			playerCreationScript.enabled = true;
			playerCam.active = true;
			mainMenuScript.enabled = false;
		}
		
		if(doWindow1)
		{
			GUI.Window(0, windowRect1, loadWindow, "Load Menu");
		}
		
		if(doWindow2)
		{
			// Flag for debug
			debugGame = true;
			playerCreationScript.enabled = true;
			playerCam.active = true;
			mainMenuScript.enabled = false;
		}
	}
	
	void loadWindow(int windowID)
	{
		if (GUI.Button(new Rect(windowRect1.width/2 - 40, windowRect1.height - 35, 80, 30), "Close"))
            doWindow1 = false;
	}

	private void readItemXML()
	{
//		if (File.Exists (Application.persistentDataPath + "/StoryItems.xml"))
//		{
//			var serializer = new XmlSerializer(typeof(MonsterContainer));
//			FileStream file = File.Open (Application.persistentDataPath + "/StoryItems.xml", FileMode.Open);
//			SaveLoad.savedGames = (List<Game>)bf.Deserialize (file);
//			file.Close ();
//		}
		XDocument xmlDoc = XDocument.Load("StoryItems.xml");
		
		foreach (XElement root in xmlDoc.Root.Elements("item"))
		{
			ItemEntity item = new ItemEntity();
			int iUses;
			
			item.key = (string)root.Element("key").Value;
			item.name = (string)root.Element("name").Value;
			item.description = (string)root.Element("description").Value;
			item.type = (string)root.Element("type").Value;
			item.equipment = (string)root.Element("equipment").Value;
			if (int.TryParse((string)root.Element("uses").Value, out iUses))
				item.uses = Convert.ToInt32((string)root.Element("uses").Value);
			else
				item.uses = 0;
			item.taken = false;
			
			storyItemMap.Add((string)root.Element("key").Value, item);
		}
	}

	private void readStoryXML()
	{
		XDocument xmlDoc = XDocument.Load("StoryExample.xml");

		List<ItemEntity> arrItems = new List<ItemEntity>();
		List<ButtonEntity> arrButtons = new List<ButtonEntity>();
		List<Condition> arrConditions = new List<Condition>();
		
		foreach (XElement scene in xmlDoc.Root.Elements("scene"))
		{
			SceneEntity eScene = new SceneEntity();
			arrItems = new List<ItemEntity>();
			arrButtons = new List<ButtonEntity>();
			arrConditions = new List<Condition>();

			// Scenes should only have one dialogue tag, this is set up for future expantions
			foreach (XElement dialogue in scene.Elements("dialogue"))
			{
				eScene.dialogue = (string)dialogue.Value;
			}

			// Scenes can have items that can be interacted with
			foreach (XElement item in scene.Elements("item"))
			{
				string key = (string)item.Attribute("itemKey");
				arrItems.Add((ItemEntity)storyItemMap[key]);
			}
			eScene.items = arrItems;	

			// Scenes can have mutiple conditions, with have to make these a switch at some point
			foreach (XElement condition in scene.Elements("condition"))
			{
				if (condition.Attribute("type").Value.Equals("hasItem"))
				{
					Condition con = new HasItemCondition((string)condition.Attribute("itemType"), (string)condition.Element("fail").Value, (string)condition.Element("success").Value, (bool)condition.Attribute("met"));
					arrConditions.Add(con);
				}
			}
			eScene.conditions = arrConditions;

			// The most complex of the XML, there can be conditions added to buttons to have them show up if needed
			foreach (XElement button in scene.Elements("button"))
			{
				ButtonEntity btn = new ButtonEntity();
				List<Condition> btnConditions = new List<Condition>();
				
				btn.text = (string)button.Attribute("text");
				btn.sceneKey = (string)button.Attribute("titleKey");
				btn.visible = (bool)button.Attribute("visible");

				// TODO: Make these into a switch later
				if (button.Attribute("action").Value.Contains("getItem"))
				{
					btn.action = (string)button.Attribute("action");
					btn.result = (string)button.Attribute("result");
				}
				if (button.Attribute("action").Value.Contains("useItem"))
				{
					btn.action = (string)button.Attribute("action");
					btn.result = (string)button.Attribute("result");
				}

				// Same conditions that are for the scene are here, maybe make into a function?
				foreach (XElement btnCondition in button.Elements("condition"))
				{
					if (btnCondition.Attribute("type").Value.Equals("hasItem"))
					{
						Condition con = new HasItemCondition((string)btnCondition.Attribute("itemType"), (string)btnCondition.Element("fail").Value, (string)btnCondition.Element("success").Value, (bool)btnCondition.Attribute("met"));
						btnConditions.Add(con);
					}
				}

				btn.conditions = btnConditions;				
				arrButtons.Add(btn);
			}
			eScene.buttons = arrButtons;

			// Add it to the map for easyer recall/indexing, use UNIQUE titles for your scenes
			storyMap.Add((string)scene.Attribute("title"),eScene);
		}
	}
	
	public Hashtable getStoryMap()
	{
		return MainMenu.storyMap;
	}

	public Hashtable getItemMap()
	{
		return MainMenu.storyItemMap;
	}

	public bool getMainDebug()
	{
		return MainMenu.debugGame;
	}
}
