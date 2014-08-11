using UnityEngine;
using System.Collections;

public class ItemEntity : MonoBehaviour
{
	public string key { get; set; }
	public string name { get; set; }
	public string description { get; set; }
	public string type { get; set; }
	public string equipment { get; set; }
	public int uses { get; set; }
	public bool taken { get; set; }
}
