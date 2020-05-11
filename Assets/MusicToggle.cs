using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggle : MonoBehaviour
{
	bool clicked;
	public GameObject checkmark;
	public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
		bool musicTogglePrefs = PlayerPrefsExt.GetBool("MusicToggle");
		AudioSourceToggle(musicTogglePrefs);
		checkmark.SetActive(musicTogglePrefs);
		clicked = musicTogglePrefs;
		CheckMarkToggle();
	}

	void AudioSourceToggle(bool toggle)
	{
		checkmark.SetActive(toggle);
		if (toggle)
		{
			audioSource.Play();
		}
		else
		{
			audioSource.Pause();
		}
	}

	public void CheckMarkToggle()
	{
		clicked = !clicked;
		if (!clicked)
		{
			checkmark.SetActive(true);
			PlayerPrefsExt.SetBool("MusicToggle", true);
			if(checkmark.activeSelf == true)
			{
				audioSource.Play();
			}
		}
		else
		{
			checkmark.SetActive(false);
			PlayerPrefsExt.SetBool("MusicToggle", false);
			if (checkmark.activeSelf == false)
			{
				audioSource.Pause();
			}
		}
	}
}

