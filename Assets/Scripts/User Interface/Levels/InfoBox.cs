using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InfoBox : MonoBehaviour
{
	public TextMeshProUGUI highscoresNumber;
	public TextMeshProUGUI levelCount;
	public GameObject[] stars;

	public int HighscoreData { get; set; }
	public int LevelCountData { get; set; }
	public int StarsData { get; set; }

	public void UpdateValues()
	{
		highscoresNumber.text = HighscoreData.ToString();
		levelCount.text = LevelCountData.ToString();
		foreach (GameObject item in stars)
		{
			item.SetActive(false);
		}
		for (int i = 0; i < StarsData; i++)
		{
			stars[i].SetActive(true);
		}
	}

	bool toggle;
	public void ToggleInfoBox()
	{
		toggle = !toggle;
		if (toggle)
		{
			Tween scaleY = transform.DOScaleY(2.3f, 0.1f);
			scaleY.Play();
			scaleY.OnStart(ShowBox);
		}
		else
		{
			Tween scaleY = transform.DOScaleY(0, 0.1f);
			scaleY.Play();
			scaleY.OnComplete(HideBox);
		}
	}
	void HideBox()
	{
		this.gameObject.SetActive(false);
	}

	void ShowBox()
	{
		this.gameObject.SetActive(true);
	}

	public void HideBoxWhenReturnToMenu()
	{
		if (transform.gameObject.activeSelf == true)
		{
			ToggleInfoBox();
		}
		else
		{
			//do nothing
		}
	}
}
