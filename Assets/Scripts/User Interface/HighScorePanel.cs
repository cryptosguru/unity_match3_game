using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HighScorePanel : MonoBehaviour
{
	public Vector2 startPos;
	public float animateToPos;

	public float animationSpeed;
    // Start is called before the first frame update
    void Start()
    {
		//startPos = this.transform.position;
    }

	bool clicked;
	public void ToggleAnimation()
	{
		clicked = !clicked;
		if (clicked)
		{
			AnimateUp();
		}
		else
		{
			AnimateToStartPos();
		}
	}

    void AnimateUp()
	{
		transform.DOLocalMoveY(animateToPos, animationSpeed);
	}

	void AnimateToStartPos()
	{
		transform.DOLocalMoveY(startPos.y, animationSpeed);
	}
}
