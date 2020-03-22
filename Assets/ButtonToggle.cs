using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
	bool clicked;
	GameObject checkmark;
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
		}
		else
		{
			checkmark.SetActive(false);
		}
	}
}
