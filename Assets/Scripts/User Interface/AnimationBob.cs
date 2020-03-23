using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class AnimationBob : MonoBehaviour
{
	[Header("Bobbing")]
	public bool bobbing;
	public Vector2 startPosition;
	public Vector2 endPosition;
	public float moveSpeed;
	bool changeBobDirection = false;
	Vector2 moveDirection;


	[Header("Wiggle")]
	public bool wiggle;
	public Vector3 startRotation;
	public Vector3 endRotation;
	public float rotationSpeed;
	bool changeRotationDirection = false;
	Vector3 moveRotation;

	private void Start()
	{
		startPosition = transform.position;
		if (bobbing)
		{
			MoveToPosition();
		}
		if (wiggle)
		{
			RotateToPosition();
		}
	}

	public void MoveToPosition()
	{
		ChangeMoveDirection();
		transform.DOMove(moveDirection, moveSpeed).SetEase(Ease.InOutSine).OnComplete(MoveToPosition);
	}

	public void RotateToPosition()
	{
		ChangeMoveRotation();
		transform.DORotate(moveRotation, rotationSpeed).SetEase(Ease.InOutSine).OnComplete(RotateToPosition);
	}

	public void ChangeMoveDirection()
	{
		changeBobDirection = !changeBobDirection;
		if (changeBobDirection)
		{
			Vector3 endPos = new Vector3(endPosition.x, endPosition.y);
			moveDirection = transform.position + endPos;
		}
		else
		{
			moveDirection = startPosition;
		}
	}

	public void ChangeMoveRotation()
	{
		changeRotationDirection = !changeRotationDirection;
		if (changeRotationDirection)
		{
			moveRotation = new Vector3(transform.rotation.x + endRotation.x, transform.rotation.y + endRotation.y, transform.rotation.z + endRotation.z);
		}
		else
		{
			moveRotation = startRotation;
		}
	}
}
