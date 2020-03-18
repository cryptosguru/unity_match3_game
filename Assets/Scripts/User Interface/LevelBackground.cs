using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LevelBackground : MonoBehaviour
{
	public Sprite[] images;
	private Sprite currentImage;
	public BackgroundColor sprites;

	public enum BackgroundColor
	{
		Red = 0,
		Blue = 1,
		Green = 2,
		Yellow = 3,
		Orange = 4
	}
	void Update()
    {
		ChangeColor(sprites);
    }
	public void ChangeColor(BackgroundColor color)
	{
		int toInt = (int)color;
		GetComponent<Image>().sprite = images[toInt];
	}
}



