using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ConditionType
{
	itemLevelReq,
	itemEquip,
	itemHas,
	playerLevel,
	playerAttribute,
	baseDefault
}

abstract class Condition : MonoBehaviour
{
	public ConditionType type;
	public bool hasBeenMet = false;
	public string failMessage = "Condition not met";
	public string successMessage = "Condition is met";
	public PlayerEntity playerStats;

	void Start ()
	{
		this.playerStats = GetComponent<PlayerEntity>();
	}

	public abstract bool IsThisConditionMet();
}

class BaseCondition : Condition
{
	ConditionType type = ConditionType.baseDefault;
	bool hasBeenMet = false;
	string failMessage = "Condition not met";
	string successMessage = "Condition is met";
	PlayerEntity playerStats;

	override public bool IsThisConditionMet()
	{
	    return false;
	}
}

// Checks if an item is carried by the player
class HasItemCondition : Condition
{
	ConditionType type = ConditionType.itemHas;
	string itemName;
	List<string> items;

	public HasItemCondition(string item, string fail, string success, bool met)
	{
	    this.itemName = item;
	    this.failMessage = fail;
	    this.successMessage = success;
	    this.hasBeenMet = met;
	}

	override public bool IsThisConditionMet()
	{
		items = StoryWindow.playerStats.items;
		if(items.Contains(this.itemName) || hasBeenMet)
	    {
	        this.hasBeenMet = true;
	        return true;
	    } else
	    {
	        return false;
	    }
	}
}
