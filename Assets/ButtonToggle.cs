using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
	bool clicked;
	GameObject checkmark;
	public GameObject gameObjectToggleAudioSource;
    // Start is called before the first frame update
    void Start()
    {
		checkmark = transform.GetChild(0).gameObject;
    }

	public void CheckMarkToggle()
	{
		clicked = !clicked;
		if (clicked)
		{
			checkmark.SetActive(true);
			gameObjectToggleAudioSource.GetComponent<AudioSource>().Pause();
		}
		else
		{
			checkmark.SetActive(false);
			gameObjectToggleAudioSource.GetComponent<AudioSource>().Play();
		}
	}
}
