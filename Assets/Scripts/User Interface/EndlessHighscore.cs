using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndlessHighscore : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI scoreTextNumber;
	[SerializeField] private LinkingSystem linkSystem;

	private void Start()
	{
		PlayerPrefs.GetInt("EndlessModeHighScore", 0);
		LoadEndlessHighscore();
	}

	public void SaveCurrentScore()
	{
		PlayerPrefs.SetInt("EndlessModeHighScore", linkSystem.GetCurrentPoints());
	}

	public void LoadEndlessHighscore()
	{
		scoreTextNumber.text = PlayerPrefs.GetInt("EndlessModeHighScore", 0).ToString();
	}
}
