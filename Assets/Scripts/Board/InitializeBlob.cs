using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBlob : MonoBehaviour
{
	public GameObject[] blobArray;
	public Vector2 gridPosition;
	public bool isSelected;
	// Start is called before the first frame update
	void Start()
    {
		CreateBlob();
    }

    void CreateBlob()
	{
		int currentBlob = Random.Range(0, blobArray.Length);
		GameObject blob = Instantiate(blobArray[currentBlob], transform.position, Quaternion.identity);
		blob.transform.parent = this.transform;
		blob.name = (blob.name);
	}
}
