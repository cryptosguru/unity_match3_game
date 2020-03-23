using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssignLevelTextNumber : MonoBehaviour
{
	public GameObject content;
	public int levelCount;
	// Start is called before the first frame update
	void Start()
    {
		content = GameObject.Find("Levels");
		if (content == null)
		{
			Debug.LogError("Couldn't Find GameObject 'content' in " + "AutoAssignLevel.cs"); 
		}
		foreach (Transform child in content.transform)
		{
			levelCount++;
			// child.GetChild(0).GetComponent<TextMeshProUGUI>().text = levelCount.ToString();
		}
    }

}
