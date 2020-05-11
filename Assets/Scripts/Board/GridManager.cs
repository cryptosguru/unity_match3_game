using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[SerializeField] private bool useCustomGrid;
	[SerializeField] private GameObject customGridParent;

	[Header("Grid Size")]
	[SerializeField] private int sizeX, sizeY;
	[SerializeField] private GameObject[,] grid;

	[Header("New Tiles Coming In")]
	[SerializeField] private float animationSpeed;
	[SerializeField] private Vector2 animateFromPos;

	[Header("Background Tile")]
	[SerializeField] private GameObject _backgroundTileNormal;
	[SerializeField] private GameObject _backgroundTileBlock;
	private Transform _backgroundTileParent;

	[Header("Custom Level")]
	[SerializeField] private TextAsset _customLevel = null;
	//the symbol for end of row
	[SerializeField] private char _rowSymbol = ';';
	//symbol between each tile
	[SerializeField] private char _tileSplitSymbol = ',';
	[SerializeField] private string _tileNormalSymbol = "0";
	[SerializeField] private string _tileBlockedSymbol = "x";
	public void CreateGrid()
	{
		grid = new GameObject[sizeX, sizeY];
		_backgroundTileParent = new GameObject("BackgroundTileParent").transform;
		_backgroundTileParent.transform.position = new Vector2(sizeX - 1, sizeY - 1) / -2f;

		if (!useCustomGrid)
		{
			for (int x = 0; x < sizeX; x++)
			{
				for (int y = 0; y < sizeX; y++)
				{
					Vector2 gridPosition = new Vector2(x, y);
					CreateNewTile(gridPosition, true, _backgroundTileNormal);
				}
			}
		}
		else
		{
			#region old custom
			/*
			for (int i = 0; i < customGridParent.transform.childCount; i++)
			{
				if(customGridParent.transform.GetChild(i).name == "Block")
				{
					Vector3 childWorldPosition = customGridParent.transform.GetChild(i).transform.position;
					Vector2 gridPosition = backgroundTileParent.transform.InverseTransformPoint(childWorldPosition);
					grid[(int)gridPosition.x, (int)gridPosition.y] = customGridParent.transform.GetChild(i).gameObject;
				}
				else
				{
					Vector2 gridPosition = backgroundTileParent.transform.InverseTransformPoint(customGridParent.transform.GetChild(i).transform.position);
					CreateNewTile(gridPosition, true);
				}
			}
			*/
			#endregion
			CreateCustomGrid(_customLevel);
		}
	}

	public void CreateCustomGrid(TextAsset customLevel)
	{
		string text = customLevel.text
			.Replace("\r", "")
			.Replace("\n", "")
			.Replace("\r\n", "");

		string[] levelRows = text.Split(_rowSymbol);
		for(int y = 0; y < levelRows.Length; y++)
		{
			int actualY = levelRows.Length - y - 1;
			string[] tileSymbols = levelRows[actualY].Split
				(_tileSplitSymbol);

			for (int x = 0; x < tileSymbols.Length; x++)
			{
				Vector2 gridPosition = new Vector2(x, y);

				if (_tileNormalSymbol == tileSymbols[x])
				{
					CreateNewTile(gridPosition, true, _backgroundTileNormal);
				}
				else if (_tileBlockedSymbol == tileSymbols[x])
				{
					CreateNewTile(gridPosition, true, _backgroundTileBlock);
				}
			}
		}
	}

	public void MoveCollum(GameObject tile)
	{
		//loop through grid
		//find empty tiles
		//move down if there is an empty tile
		//if the the tile below is blocked
		//then dont move down

		int tileX = (int)tile.transform.position.x;
		int tileY = (int)_backgroundTileParent.position.y;
		int lowestPos = tileY;

		bool foundEmptyTile = false;
		for (int y = tileY; y <= tileY + sizeY; y++)
		{
			GameObject currentTile = GetObjectAtWorldPosition(new Vector2(tileX, y));
			if (currentTile == null)
			{
				if (foundEmptyTile == false)
				{
					lowestPos = y;
					foundEmptyTile = true;
				}
				continue;
			}
			if (foundEmptyTile)
			{
				Debug.DrawLine(currentTile.transform.position, new Vector2(tileX, lowestPos), Color.red, 10);
				if(currentTile == _backgroundTileBlock)
				{
					continue;
				}
				else
				{
					MoveTo(currentTile, new Vector2(tileX, y), new Vector2(tileX, lowestPos));
				}

				//doesnt find right pos

				y = lowestPos;
				foundEmptyTile = false;
			}
			if (currentTile != null)
			{
				lowestPos += 1;
			}
			
		}
		CreateNewTile(new Vector2(_backgroundTileParent.InverseTransformPoint(tile.transform.position).x, sizeY - 1), true, _backgroundTileNormal);
		/*
		int tileX = (int)tile.transform.position.x;
		int tileY = (int)_backgroundTileParent.position.y;
		int lowestPos = tileY;

		//Debug.Log("lowestPosition " + lowestPos);
		bool foundEmptyTile = false;
		for (int y = tileY; y <= tileY + sizeY; y++)
		{
			GameObject currentTile = GetObjectAtWorldPosition(new Vector2(tileX, y));
			if(currentTile == null)
			{
				if (foundEmptyTile == false)
				{
					lowestPos = y;
					foundEmptyTile = true;
				}
				continue;
			}
			if (foundEmptyTile)
			{
				Debug.DrawLine(currentTile.transform.position, new Vector2(tileX, lowestPos), Color.red, 10);
				//doesnt find right pos
				MoveTo(currentTile, new Vector2(tileX, y), new Vector2(tileX, lowestPos));

				y = lowestPos;
				foundEmptyTile = false;
			}
			if(currentTile != null)
			{
				lowestPos += 1;
			}
		}
		CreateNewTile(new Vector2(_backgroundTileParent.InverseTransformPoint(tile.transform.position).x, sizeY - 1), true, _backgroundTileNormal);
		*/
	}

	public void ClearGrid()
	{
		if(_backgroundTileParent != null)
		{
			Destroy(_backgroundTileParent.gameObject);
		}
	}
	public void CreateNewTile(Vector2 gridPosition, bool animate, GameObject prefab)
	{
		Vector2 worldPos = _backgroundTileParent.TransformPoint(gridPosition);

		Vector2 startPosition = new Vector2(worldPos.x, sizeY / 2);
		GameObject currentTile = Instantiate(prefab, gridPosition, Quaternion.identity);

		currentTile.GetComponent<InitializeBlob>().gridPosition = gridPosition;
		currentTile.transform.parent = _backgroundTileParent;
		currentTile.transform.name = gridPosition.ToString();

		grid[(int)gridPosition.x, (int)gridPosition.y] = currentTile;

		if (animate)
		{
			//StartCoroutine(MoveObjectFromTo(currentTile, startPosition, worldPos));
			MoveTo(currentTile, startPosition, worldPos);
		}
	}

	public void MoveTo(GameObject tile, Vector2 startPosition, Vector2 targetPosition)
	{
		Vector3 localStart = _backgroundTileParent.InverseTransformPoint(startPosition);
		Vector3 localTargetPosition = _backgroundTileParent.InverseTransformPoint(targetPosition);
		//Debug.Log($"MovingObjectFromTo{localStart}to {localTargetPosition}");

		grid[(int)localStart.x, (int)localStart.y] = null;
		grid[(int)localTargetPosition.x, (int)localTargetPosition.y] = tile;

		tile.transform.position = startPosition + animateFromPos;
		tile.GetComponent<PositionBlender>().StartMove(targetPosition, animationSpeed);
	}

	public IEnumerator MoveObjectFromTo(GameObject tile, Vector2 startPosition, Vector2 targetPosition)
	{
		float step = (animationSpeed / Vector3.Distance(startPosition, targetPosition) * Time.deltaTime);
		float time = 0;
		while (time <= 1.0f)
		{
			time += step;
			if(tile == null)
			{
				yield break;
			}
			tile.transform.position = Vector3.Lerp(startPosition, targetPosition, time);

			yield return null;
		}

		tile.transform.position = targetPosition;
	}
	
	public List<GameObject> GetNeighbours(Vector2 position)
	{
		List<GameObject> neighbours = new List<GameObject>();
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
				{
					continue;
				}

				Vector3 worldPos = new Vector2(position.x + x, position.y + y);
				GameObject neighbour = GetObjectAtWorldPosition(worldPos);

				if (neighbour != null)
				{
					neighbours.Add(neighbour);
				}
			}
		}
		//Debug.Log(neighbours.Count);
		return neighbours;
	}

	public GameObject GetObjectAtPosition(int x, int y)
	{
		if(x >= sizeX || y >= sizeY || x < 0 || y < 0)
		{
			Debug.LogWarning("object was outside grid");
			return null;
		}
		if(grid[x, y] != null)
		{
			return grid[x, y];
		}
		else
		{
			return null;
		}
	}

	public GameObject GetObjectAtWorldPosition(Vector2 position)
	{
		Vector2 localPosition = _backgroundTileParent.InverseTransformPoint(position);
		return GetObjectAtPosition((int)localPosition.x, (int)localPosition.y);
	}

	public void DestroyTile(GameObject tile, float time)
	{
		Destroy(tile, time);
		Vector2 localPosition = _backgroundTileParent.InverseTransformPoint(tile.transform.position);
		grid[(int)localPosition.x, (int)localPosition.y] = null;
		Debug.Log(grid[(int)localPosition.x, (int)localPosition.y]);
	}
}
