using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinkingSystem : MonoBehaviour
{
	[Header("Line Renderer Gameobject")]
	public LineRenderer line;

	[Header("Modify Match Rules")]
	public int minRequiredMatch;

	//--------------------------//

	private List<Vector2> listOfPostions = new List<Vector2>();
	public List<GameObject> listOfTiles = new List<GameObject>();

	private GridManager grid;

	[HideInInspector]
	public int scoredPoints { get; set; }
	private GameObject lastObject;
	private string matchID;
	private bool clickedObject;
	private int sameColumAmount = 1;

	private void Start()
	{
		grid = GameObject.Find("Grid").GetComponent<GridManager>();
		if(grid == null)
		{
			Debug.LogError("reference to grid from linkingsystem can not be found & is = " + grid);
		}
	}

	public int GetCurrentPoints()
	{
		return scoredPoints;
	}

	private void Update()
	{
		DrawLine();
	}

	public void OnClick(GameObject objectCurrentlySelected)
	{
		clickedObject = !clickedObject;
		if (clickedObject)
		{
			string _colorID = objectCurrentlySelected.GetComponentInChildren<BlobBase>().colorID;

			lastObject = objectCurrentlySelected;
			matchID = _colorID;
			OnEnter(objectCurrentlySelected);
		}
	}

	public void OnEnter(GameObject objectCurrentlySelected)
	{
		if (lastObject && IsColorNearby(objectCurrentlySelected))
		{
			if (AvailablePositionAround(objectCurrentlySelected))
			{
				lastObject = objectCurrentlySelected;
				objectCurrentlySelected.GetComponent<InitializeBlob>().isSelected = true;
				AddPosition(objectCurrentlySelected);
			}
			else
			{
				RemoveLastPosition();
			}
		}
	}

	void OrderTiles()
	{
		listOfTiles = listOfTiles.OrderBy(tile => -tile.transform.position.y).ToList();
	}
	public void OnRelease()
	{
		OrderTiles();
		if (listOfTiles.Count >= minRequiredMatch)
		{
			for (int i = 0; i < listOfTiles.Count; i++)
			{
				Debug.Log("Move Collum");
				grid.DestroyTile(listOfTiles[i].gameObject, 0.0f);
				grid.MoveCollum(listOfTiles[i]);
			}
			scoredPoints += listOfTiles.Count;
			PlayReleaseSound();
		}
		for (int i = 0; i < listOfTiles.Count; i++)
		{
			listOfTiles[i].GetComponent<InitializeBlob>().isSelected = false;
		}
		clickedObject = false;
		ClearLists();
	}

	public GameObject[] releaseSFX;
	private int lastEffect;
	void PlayReleaseSound()
	{
		var curEffect = Random.Range(0, releaseSFX.Length);
		if(curEffect != lastEffect)
		{
			releaseSFX[curEffect].GetComponent<AudioSource>().Play();
		}
		else
		{
			curEffect = Random.Range(0, releaseSFX.Length);
			PlayReleaseSound();
		}
		lastEffect = curEffect;
	}

	public GameObject hoverSFX;
	void PlayHoverSound()
	{
		hoverSFX.GetComponent<AudioSource>().Play();
	}

	public void OnExit(GameObject objectCurrentlySelected)
	{
		lastObject = objectCurrentlySelected;
	}

	bool AvailablePositionAround(GameObject objectCurrentlySelected)
	{
		string selectedColorID = objectCurrentlySelected.GetComponentInChildren<BlobBase>().colorID;
		bool isSelected = objectCurrentlySelected.GetComponent<InitializeBlob>().isSelected;
		if (!isSelected && selectedColorID == matchID && clickedObject && IsColorNearby(objectCurrentlySelected) && LastObjectHasMatchID(objectCurrentlySelected))
		{
			Debug.Log("pressed");
			return true;
		}
		else
		{
			return false;
		}
	}

	bool IsColorNearby(GameObject objectCurrentlySelected)
	{
		List<GameObject> neighbours = grid.GetNeighbours(objectCurrentlySelected.transform.position);
		for (int i = 0; i < neighbours.Count; i++)
		{
			BlobBase blobBase = neighbours[i].GetComponentInChildren<BlobBase>();
			if(neighbours[i] != null && blobBase != null && blobBase.colorID != null)
			{
				if (blobBase.colorID == matchID)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool LastObjectHasMatchID(GameObject objectCurrentlySelected)
	{
		//check entire list
		if(lastObject.GetComponentInChildren<BlobBase>().colorID == matchID && IsColorNearby(objectCurrentlySelected))
		{
			return true;
		}
		clickedObject = false;
		return false;
	}
	
	
	void AddPosition(GameObject listObject)
	{
		listOfPostions.Add(listObject.transform.position);
		listOfTiles.Add(listObject);
	}
	void RemoveLastPosition()
	{
		lastObject.GetComponent<InitializeBlob>().isSelected = false;
		listOfTiles.Remove(lastObject);
		listOfPostions.Remove(lastObject.transform.position);
	}
	void ClearLists()
	{
		listOfPostions.Clear();
		listOfTiles.Clear();
		line.positionCount = 0;
	}
	public void DrawLine()
	{
		line.positionCount = listOfPostions.Count;
		for (int i = 0; i < line.positionCount; i++)
		{
			line.SetPosition(i, listOfPostions[i]);
		}
	}
}
