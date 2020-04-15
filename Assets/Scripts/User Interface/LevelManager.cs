using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public List<GameObject> listOfLevels = new List<GameObject>();
	private GameObject currentLevelPrefab;
	// Start is called before the first frame update

	public void SelectLevel(int level)
	{
		if(currentLevelPrefab != null)
		{
			currentLevelPrefab = listOfLevels[level];
		}
		else
		{
			Debug.LogWarning("level is not set");
		}
		Debug.Log("level selected " + level);
	}
}
