using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[Header("Grid Size")]
	public int sizeX, sizeY;
	public GameObject[,] grid;

	[Header("New Tiles Coming In")]
	public float animationSpeed;
	public Vector2 animateFromPos;
	[Header("Background Tile")]
	public GameObject backgroundTilePrefab;
	private Transform backgroundTileParent;

	public void CreateGrid()
	{
		grid = new GameObject[sizeX, sizeY];
		backgroundTileParent = new GameObject("BackgroundTileParent").transform;
		backgroundTileParent.transform.position = new Vector2(sizeX - 1, sizeY - 1) / -2f;

		for (int x = 0; x < sizeX; x++)
		{
			for (int y = 0; y < sizeX; y++)
			{
				Vector2 gridPosition = new Vector2(x, y);
				CreateNewTile(gridPosition, true);
			}
		}
	}

	public void MoveCollum(GameObject tile)
	{
		int tileX = (int)tile.transform.position.x;
		int tileY = (int)backgroundTileParent.position.y;
		int lowestPos = tileY;
		bool hasFoundEmpty = false;
		for (int y = tileY; y <= tileY + sizeY ; y++)
		{
			GameObject currentTile = GetObjectAtWorldPosition(new Vector2(tileX, y));
			if(currentTile == null)
			{
				if (!hasFoundEmpty)
				{
					lowestPos = y;
					hasFoundEmpty = true;
				}
				continue;
			}
			if (hasFoundEmpty)
			{
				Debug.DrawLine(currentTile.transform.position, new Vector2(tileX, lowestPos), Color.red, 10);
				MoveTo(currentTile, new Vector2(tileX, y), new Vector2(tileX, lowestPos));

				y = lowestPos;
				hasFoundEmpty = false;
			}
		}
		CreateNewTile(new Vector2(backgroundTileParent.InverseTransformPoint(tile.transform.position).x, sizeY - 1), true);
	}

	public void ClearGrid()
	{
		Destroy(backgroundTileParent.gameObject);
	}
	public void CreateNewTile(Vector2 gridPosition, bool animate)
	{
		Vector2 worldPos = backgroundTileParent.TransformPoint(gridPosition);

		Vector2 startPosition = new Vector2(worldPos.x, sizeY / 2);
		GameObject currentTile = Instantiate(backgroundTilePrefab, gridPosition, Quaternion.identity);

		currentTile.GetComponent<InitializeBlob>().gridPosition = gridPosition;
		currentTile.transform.parent = backgroundTileParent;
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
		Vector3 localStart = backgroundTileParent.InverseTransformPoint(startPosition);
		Vector3 localTargetPosition = backgroundTileParent.InverseTransformPoint(targetPosition);

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
		return neighbours;
	}

	public GameObject GetObjectAtPosition(int x, int y)
	{
		if(x >= sizeX || y >= sizeY || x < 0 || y < 0)
		{
			Debug.LogWarning("object was outside grid");
			return null;
		}
		return grid[x, y];
	}

	public GameObject GetObjectAtWorldPosition(Vector2 position)
	{
		Vector2 localPosition = backgroundTileParent.InverseTransformPoint(position);
		return GetObjectAtPosition((int)localPosition.x, (int)localPosition.y);
	}

	public void DestroyTile(GameObject tile, float time)
	{
		Destroy(tile, time);
		Vector2 localPosition = backgroundTileParent.InverseTransformPoint(tile.transform.position);
		grid[(int)localPosition.x, (int)localPosition.y] = null;
		Debug.Log(grid[(int)localPosition.x, (int)localPosition.y]);
	}
}
