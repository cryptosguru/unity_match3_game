using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
	public TextMeshProUGUI number;
	public LinkingSystem link;
    // Update is called once per frame
    void Update()
    {
		number.text = link.GetCurrentPoints().ToString();
    }
}
