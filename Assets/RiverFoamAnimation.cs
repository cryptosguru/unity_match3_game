using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverFoamAnimation : MonoBehaviour
{
	public GameObject[] foam;

	public Vector2 firstStartPos;
	public Vector2 secondStartPos;

	public Vector2 endpos;

	public bool animate;
	public float animationSpeed;
	private void Start()
	{
		firstStartPos = foam[0].gameObject.transform.position;
		secondStartPos = foam[1].gameObject.transform.position;
	}

	void Update()
	{
		if (animate)
		{
			for (int i = 0; i < foam.Length; i++)
			{
				foam[i].transform.position += new Vector3(endpos.x * animationSpeed * Time.deltaTime, transform.position.y);
				Vector2 screenPosition = Camera.main.WorldToScreenPoint(foam[i].transform.position);
				if (foam[i].transform.position.x < -screenPosition.x + endpos.x)
				{
					foam[i].transform.position = secondStartPos;
				}
			}
		}
	}

	
}
