using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLockState : MonoBehaviour
{
	public bool isLocked;
	private GameObject lockImage;

	private void Start()
	{
		lockImage = this.gameObject.transform.Find("lockImage").gameObject;
		if(lockImage == null)
		{
			Debug.LogError("lockImage was not found");
		}
	}
	// Update is called once per frame
	void Update()
	{
		if (isLocked)
		{
			lockImage.SetActive(true);
		}
		else
		{
			lockImage.SetActive(false);
		}

	}
}
