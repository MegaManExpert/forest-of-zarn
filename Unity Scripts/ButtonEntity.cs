using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class ButtonEntity : MonoBehaviour
{
	public string text {get; set;}
	public string sceneKey { get; set; }
	public string action { get; set; }
	public string result { get; set; }
	public bool visible { get; set; }
	public List<Condition> conditions { get; set; }
}
