using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class StoryWindow : MonoBehaviour
{
	Vector2 scrollPosition = Vector2.zero;
	Vector2 scrollPosition1 = Vector2.zero;

	public GUISkin StoneGuiSkin;
	public static PlayerEntity playerStats;

	static Hashtable storyMap = new Hashtable();
	static Hashtable itemMap = new Hashtable();
	SceneEntity eScene = new SceneEntity();
	ItemEntity eItem = new ItemEntity();

	// Current scene the button is tied to from the key (Title of scene)
	// Use "start" for the title for the intro scene
	string currentKey = "start";
	List<ButtonEntity> nextBtns = new List<ButtonEntity>();	
	int sceneBtnSize;
	
	string sDialogue = "";
	string sAction = "";
	
	Rect windowSelect = new Rect(Screen.width/2 - 125, Screen.height/2 - 75, 250, 155);	
	bool doWindowSelect = false;
	
	// Use this for initialization
	void Start ()
	{
		playerStats = GetComponent<PlayerEntity>();
		storyMap = GetComponent<MainMenu>().getStoryMap();
		itemMap = GetComponent<MainMenu>().getItemMap();
		eScene = (SceneEntity)storyMap[currentKey];
		sDialogue = eScene.dialogue;
		sceneBtnSize = eScene.buttons.Count;
		foreach(ButtonEntity btn in eScene.buttons)
		{
			if(btn.visible)
				nextBtns.Add(btn);
		}
		currentKey = onButtonClick(currentKey, 0);
		returnBtnMap(currentKey, 0);
	}
	
	void OnGUI()
	{
		GUI.skin = StoneGuiSkin;

		GUI.Box(new Rect(185,45,Screen.width-200,Screen.height-135),"");
		GUILayout.BeginArea(new Rect(200,65,Screen.width-235,Screen.height-170));
	    scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width-240), GUILayout.Height(Screen.height-175));
	    
		GUILayout.Label(sDialogue);
	    GUILayout.EndScrollView ();
	    GUILayout.EndArea();
		
		GUI.BeginGroup (new Rect(10,40,175,Screen.height));
		GUI.Box(new Rect(5,5,160,Screen.height-135), "Player Stats");
		GUI.Label(new Rect (20,45,145,30), "Name: " + playerStats.getName());
		GUI.Label(new Rect (20,60,145,30), "Race: " + playerStats.getRaceType());
		GUI.Label(new Rect (20,75,145,30), "Hit Points: " + playerStats.getHitPoints());
		GUI.Label(new Rect (20,90,145,30), "Endurence: " + playerStats.getEndurence());
		GUI.EndGroup();
		
		GUI.BeginGroup (new Rect (20, 5, 540, 40));
		GUI.Button (new Rect (0,0,85,30), "Test 1");
		GUI.Button (new Rect (90,0,85,30), "Test 2");
		GUI.Button (new Rect (180,0,85,30), "Test 3");
		GUI.Button (new Rect (270,0,85,30), "Test 4");
		GUI.Button (new Rect (360,0,85,30), "Test 5");
		GUI.Button (new Rect (450,0,85,30), "Test 6");
		GUI.EndGroup();
		
		GUI.BeginGroup (new Rect (20, Screen.height - 70, 540, 70));
		if(sceneBtnSize > 0)
			if(GUI.Button (new Rect (0,0,85,30), nextBtns[0].text))
			{
				currentKey = onButtonClick(nextBtns[0].sceneKey, 0);
				returnBtnMap(currentKey, 0);
				//Debug.Log(nextBtns[0].sceneKey);				
				
				//doWindowSelect = true;
			}
		if(sceneBtnSize > 1)
			if(GUI.Button (new Rect (90,0,85,30), nextBtns[1].text))
			{
				currentKey = onButtonClick(nextBtns[1].sceneKey, 1);
				returnBtnMap(currentKey, 1);
			}
		if(sceneBtnSize > 2)
			if(GUI.Button (new Rect (180,0,85,30), nextBtns[2].text))
			{
				currentKey = onButtonClick(nextBtns[2].sceneKey, 2);
				returnBtnMap(currentKey, 2);

			}
		if(sceneBtnSize > 3)
			if(GUI.Button (new Rect (270,0,85,30), nextBtns[3].text))
			{
				currentKey = onButtonClick(nextBtns[3].sceneKey, 3);
				returnBtnMap(currentKey, 3);
				
			}
		if(sceneBtnSize > 4)
			if(GUI.Button (new Rect (360,0,85,30), nextBtns[4].text))
			{
				currentKey = onButtonClick(nextBtns[4].sceneKey, 4);
				returnBtnMap(currentKey, 4);
			}
		if(sceneBtnSize > 5)
			if(GUI.Button (new Rect (450,0,85,30), nextBtns[5].text))
			{
				currentKey = onButtonClick(nextBtns[5].sceneKey, 5);
				returnBtnMap(currentKey, 5);
			}
		if(sceneBtnSize > 6)
			if(GUI.Button (new Rect (0,35,85,30), nextBtns[6].text))
			{
				currentKey = onButtonClick(nextBtns[6].sceneKey, 6);
				returnBtnMap(currentKey, 6);
			}
		if(sceneBtnSize > 7)
			if(GUI.Button (new Rect (90,35,85,30), nextBtns[7].text))
			{
				currentKey = onButtonClick(nextBtns[7].sceneKey, 7);
				returnBtnMap(currentKey, 7);
			}
		if(sceneBtnSize > 8)
			if(GUI.Button (new Rect (180,35,85,30), nextBtns[8].text))
			{
				currentKey = onButtonClick(nextBtns[8].sceneKey, 8);
				returnBtnMap(currentKey, 8);
			}
		if(sceneBtnSize > 9)
			if(GUI.Button (new Rect (270,35,85,30), nextBtns[9].text))
			{
				currentKey = onButtonClick(nextBtns[9].sceneKey, 9);
				returnBtnMap(currentKey, 9);
			}
		if(sceneBtnSize > 10)
			if(GUI.Button (new Rect (360,35,85,30), nextBtns[10].text))
			{
				currentKey = onButtonClick(nextBtns[10].sceneKey, 10);
				returnBtnMap(currentKey, 10);
			}
		if(sceneBtnSize > 11)
			if(GUI.Button (new Rect (450,35,85,30), nextBtns[11].text))
			{
				currentKey = onButtonClick(nextBtns[11].sceneKey, 11);
				returnBtnMap(currentKey, 11);
			}
		GUI.EndGroup();
		
		if(doWindowSelect)
		{
			GUI.Window(0, windowSelect, reactWindow, "Action");
		}
	}
	
	void reactWindow(int windowID)
	{
		GUILayout.BeginArea(new Rect(10,20,windowSelect.width-15,windowSelect.height-60));
	    scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1, GUILayout.Width(windowSelect.width-15), GUILayout.Height(windowSelect.height-60));
		GUILayout.Label(sAction);
	    GUILayout.EndScrollView ();
	    GUILayout.EndArea();
		
		if (GUI.Button(new Rect(windowSelect.width/2 - 20, windowSelect.height - 35, 40, 30), "Next"))
		{
            doWindowSelect = false;
		}
	}

	private bool AreAllConditionsMet(string sceneKey, int btnIndex)
	{
		bool result = eScene.buttons[btnIndex].visible;
		string responseMessage = "";

		foreach (Condition condition in eScene.buttons[btnIndex].conditions)
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
			sDialogue += responseMessage;
			result = conResult;			
		}
		return result;
	}

	// Button acttion parseing for currently loaded scene
	private string onButtonClick(string nextSceneKey, int btnIndex)
	{
		//Debug.Log ("Button Index: " + btnIndex + "; Button Count: " + eScene.buttons.Count);
		string actionTrigger = "";

		//Parts that will be used for button actions that are not part of the display conditions
		ItemEntity item = new ItemEntity();
		List<string> playerItems = new List<string>();

		// NOTE: I use nextBtns since it's the current list of visable buttons
		// which gets re-populated from a list of next list of buttons that are
		// visable

		// Break out the action and the attched data check
		string[] btnAction = {""};
		if (nextBtns[btnIndex].action != null)
		{
			if (nextBtns[btnIndex].action.Contains(":"))
			{
				// TODO: Fix for a list of attached data for more flexablity in actions
				btnAction = nextBtns[btnIndex].action.Split(':');
				actionTrigger = btnAction[1];
			}
			else
				btnAction[0] = nextBtns[btnIndex].action;
		}
		
		switch (btnAction[0])
		{
		case "getItem":
			// Check to see if player has any items, if so get them
			if (playerStats.items != null)
				playerItems = playerStats.items;
			
			// Add item to curent invatory
			actionTrigger = btnAction[1];
			playerItems.Add(actionTrigger);
			playerStats.items = playerItems;
			
			// Hide items from lists
			nextBtns[btnIndex].visible = false;
			item = (ItemEntity)itemMap[actionTrigger];
			item.taken = true;
			itemMap[actionTrigger] = item;
			sAction = "You have taken the " + item.name + ".";
			doWindowSelect = true;

			// Force a refresh of the current list
			//returnBtnMap(currentKey, btnIndex);

			break;
			
		case "useItem":
			if (playerStats.items != null)
			{
				playerItems = playerStats.items;
				item = (ItemEntity)itemMap[actionTrigger];
				
				if (playerItems.Contains(item.key) && item.uses > 0)
				{
					nextSceneKey = eScene.buttons[btnIndex].result;
				}
				
				playerStats.items = playerItems;
			}
			break;
			
		default:
			break;
		}

		return nextSceneKey;
	}

	private void returnBtnMap(string sceneKey, int btnIndex)
	{	
		SceneEntity scene = (SceneEntity)storyMap[sceneKey];
		//Debug.Log ("Button Index: " + btnIndex + "; Scene Title: " + sceneKey + "; Button Count: " + scene.buttons.Count);
		sDialogue = scene.dialogue;
		
		// Tokenize the item tags as a list and is able to be pulled apart for later
		string itemList = "";
		foreach(ItemEntity item in scene.items)
		{
			if (!item.taken)
				itemList += " " + item.name + ",";
		}
		itemList = itemList.TrimEnd(',');
		sDialogue = sDialogue.Replace("@items", itemList);
		
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
			sDialogue = sDialogue.Replace("@item" + itemIndex, itemObject);
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
			sDialogue = sDialogue.Replace("@condition" + condIndex, responseMessage);
		}
		
		//AreAllConditionsMetForWindow(sceneKey); <-- Old but might be useful later for actions

		// Find all the buttons and get all there key pointers
		eScene = scene;

		// Create a list of temp buttons and add the visable ones to it
		List<ButtonEntity> tempBtns = new List<ButtonEntity>();
		// Take the currently visable buttons and check if they are have any meet requirements to be visable
		for (int idxBtn=0; idxBtn < eScene.buttons.Count; idxBtn++)
		{
			eScene.buttons[idxBtn].visible = AreAllConditionsMet(sceneKey, idxBtn);
			//Debug.Log ("Name: " + eScene.buttons[idxBtn].text + "; Key: " + eScene.buttons[idxBtn].sceneKey +
			//          "; Action: " + eScene.buttons[idxBtn].action + "; Result: " + eScene.buttons[idxBtn].result +
			//          "; Visible: " + eScene.buttons[idxBtn].visible + "; Cond. Count: " + eScene.buttons[idxBtn].conditions.Count +
			//          "; Button Index " + idxBtn);
			if(eScene.buttons[idxBtn].visible)
				tempBtns.Add(eScene.buttons[idxBtn]);
			//Debug.Log ("tempBtns: " + tempBtns.Count);
		}
		nextBtns = tempBtns;
		sceneBtnSize = nextBtns.Count;
		//Debug.Log (sceneBtnSize);
	}
}