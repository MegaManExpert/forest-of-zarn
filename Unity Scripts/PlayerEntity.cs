using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEntity : MonoBehaviour
{
	string name;
	
	int gender; // Male(0), Female(1), genderless(2) 
	enum Race {Human = 0, Reptilian = 1, Feline = 2, Lupine = 3, Equestrian = 4, Angelic = 5, Demonic = 6};
	Race raceType;
	int strength;
	int vitality;
	int intelligence;
	int mentality;

	int hitpoints;
	int endurence;
	int weapon;
	
	float thin;
	float heavy;
	float build;

	bool isDebug;

	public List<string> items;
	
	void Start ()
	{
		name = "";	
		gender = 3;
		raceType = Race.Human;
		strength = 10;
		vitality = 10;
		intelligence = 10;
		mentality = 10;
		hitpoints = 10;
		endurence = 10;
		weapon = 5;	
		thin = 0.0f;
		heavy = 0.0f;
		build = 0.0f;
		items = new List<string>();
		isDebug = false;
	}
	
	public string getName()
	{
		return this.name;
	}
	
	public string getRaceType()
	{
		return this.raceType.ToString();
	}
	
	public int getHitPoints()
	{
		return this.hitpoints;
	}
	
	public int getEndurence()
	{
		return this.endurence;
	}
	
	public void setName(string chrName)
	{
		this.name = chrName;
	}
	
	public void setRaceType(int chrType)
	{
		this.raceType = (Race)chrType;
	}

	public void setHitPoints(int chrHP)
	{
		this.hitpoints = chrHP;
	}

	public void setEndurence(int chrED)
	{
		this.endurence = chrED;
	}

	public void setThin(float thinB)
	{
		this.thin = thinB;
	}

	public void setHeavy(float heavyB)
	{
		this.heavy = heavyB;
	}

	public void setBuild(float buildB)
	{
		this.build = buildB;
	}

	public void setDebug(bool debug)
	{
		this.isDebug = debug;
	}
}
