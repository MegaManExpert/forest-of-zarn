using UnityEngine;
using System.Collections;

public class ViewPortButtons : MonoBehaviour
{
	GameObject player;
	GameObject mainCamera;
	Quaternion defaultRotation = new Quaternion(0,0,0,0);
	PlayerCreation pc;

	// This is for resetting player to the default
	// postion and rotation.
	void Start()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		pc = mainCamera.GetComponent<PlayerCreation>();
//		Debug.Log(mainCamera + ":" + pmd + "/" + pc);
    }
	
	void OnGUI()
	{
		float w = 50.0f;
		float h = 20.0f;
		float x = (Screen.width/1.25f) - (w/2);
		float y = (Screen.height/2) - (h/2);
		
		if(GUI.Button(new Rect(x, Screen.height-30, w, h), "Reset"))
		{			
			player.transform.localEulerAngles = new Vector3(0,0,0);
			player.transform.localPosition = new Vector3(0,1,0);			
		}
	}
}
