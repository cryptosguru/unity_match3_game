using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PositionBlender : MonoBehaviour
{
	private bool isMoving;
	private Vector3 targetPosition;
	private float timeLeft;
	public void StartMove(Vector3 targetPosition, float time)
	{
		isMoving = true;
		timeLeft = time;
		this.targetPosition = targetPosition;
	}
	private void Update()
	{
		if (isMoving)
		{
			transform.position = Vector3.Lerp(transform.position, targetPosition, timeLeft * Time.deltaTime);
			//LeanTween.move(this.gameObject, targetPosition, timeLeft * Time.deltaTime);
			timeLeft -= Time.deltaTime;
			if(timeLeft < 0)
			{
				isMoving = false;
				transform.position = targetPosition;
			}
		}
	}
}
