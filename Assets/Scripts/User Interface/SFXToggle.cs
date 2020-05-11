using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXToggle : MonoBehaviour
{
	bool clicked;
	public GameObject checkmark;
	public GameObject sfx;
	// Start is called before the first frame update
	void Start()
	{
		bool sfxTogglePrefs = PlayerPrefsExt.GetBool("SFXToggle");
		sfx.SetActive(sfxTogglePrefs);
		checkmark.SetActive(sfxTogglePrefs);
		clicked = !clicked;
	}

	public void CheckMarkToggle()
	{
		clicked = !clicked;
		if (!clicked)
		{
			sfx.SetActive(false);
			PlayerPrefsExt.SetBool("SFXToggle", false);
			if(sfx.activeSelf == false)
			{
				checkmark.SetActive(false);
			}
		}
		else
		{
			sfx.SetActive(true);
			PlayerPrefsExt.SetBool("SFXToggle", true);
			if (sfx.activeSelf == true)
			{
				checkmark.SetActive(true);
			}
		}
	}
}

public class PlayerPrefsExt
{
	public static void SetBool(string key, bool state)
	{
		PlayerPrefs.SetInt(key, state ? 1 : 0);
	}

	public static bool GetBool(string key)
	{
		int value = PlayerPrefs.GetInt(key);

		if (value == 1)
		{
			return true;
		}

		else
		{
			return false;
		}
	}
}
