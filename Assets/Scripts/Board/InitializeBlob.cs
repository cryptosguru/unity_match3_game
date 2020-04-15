using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBlob : MonoBehaviour
{
	public GameObject[] blobArray;
	public Vector2 gridPosition;
	public bool isSelected;
	private GridManager grid;

	void Start()
    {
		CreateBlob();
		grid = GameObject.Find("Grid").GetComponent<GridManager>();
    }

	bool color;
    void CreateBlob()
	{
		int currentBlob = Random.Range(0, blobArray.Length);
		GameObject blob = Instantiate(blobArray[currentBlob], transform.position, Quaternion.identity);		blob.transform.parent = this.transform;
		blob.name = (blob.name);
	}
}
