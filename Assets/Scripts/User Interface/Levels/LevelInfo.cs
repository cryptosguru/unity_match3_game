using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
	//info of what has been achieved for each level

	public int highscore;
	[Range(0,3)]
	public int stars; //total of 3 stars ex. 0/3
	public int levelCount;

	public GameObject InfoBox;

	public void PassValuesToInfoBox()
	{
		var info = InfoBox.GetComponent<InfoBox>();
		info.HighscoreData = highscore;
		info.LevelCountData = levelCount;
		info.StarsData = stars;
		info.UpdateValues();
	}
}
