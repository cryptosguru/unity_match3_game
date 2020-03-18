using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private AnimationHandler animationHandler;

	[Header("ResizeButton")]
	public bool buttonAnimation;
	public float scaleAnimationTime;
	public Vector2 endScale;
	public Vector2 startScale;

	private void Start()
	{
		animationHandler = GameObject.Find("AnimationHandler").GetComponent<AnimationHandler>();
	}

	public void Resize(Vector2 size)
	{
		if (buttonAnimation)
		{
			LeanTween.scale(this.gameObject, size,scaleAnimationTime);
		}
	}

	//<summary>
	//Button events
	#region event data
	public void OnPointerEnter(PointerEventData eventData)
	{
		Resize(startScale);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Resize(endScale);
	}

	#endregion
}
