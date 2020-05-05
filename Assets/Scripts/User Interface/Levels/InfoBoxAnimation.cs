using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfoBoxAnimation : MonoBehaviour
{
    void ScaleUp()
	{
		transform.DOScale(1, 1);
	}

	void ScaleDown()
	{
		transform.DOScale(0, 1);
	}
}
