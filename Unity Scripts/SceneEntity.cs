using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class SceneEntity : MonoBehaviour
{
	public string dialogue { get; set; }
	public List<ItemEntity> items { get; set; }
	public List<ButtonEntity> buttons { get; set; }
	public List<Condition> conditions { get; set; }
}
