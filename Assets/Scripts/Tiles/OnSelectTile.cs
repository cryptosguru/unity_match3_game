using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSelectTile : MonoBehaviour
{
	private SpriteRenderer spriteRender;
	public LinkingSystem link;

	private void Start()
	{
		spriteRender = GetComponent<SpriteRenderer>();
		link = GameObject.Find("LinkingSystem").GetComponent<LinkingSystem>();
		if(link == null)
		{
			Debug.LogError("LinkingSystem not found");
		}
	}

	void TileBackgroundColor(Color color)
	{
		spriteRender.color = new Color(color.r, color.g, color.b, 0.3f);
	}

	//<summary>
	//mouse events
	private void OnMouseEnter()
	{
		TileBackgroundColor(Color.white);
		link.OnEnter(this.gameObject);
	}
	private void OnMouseExit()
	{
		TileBackgroundColor(Color.black);
		link.OnExit(this.gameObject);
	}
	private void OnMouseDown()
	{
		TileBackgroundColor(Color.yellow);
		link.OnClick(this.gameObject);
	}

	private void OnMouseUp()
	{
		link.OnRelease();
	}
}
