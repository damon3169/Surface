using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerChange : MonoBehaviour
{

	public string SortingLayerName = "Default";
	public int SortingOrder = 20;

	void Awake()
	{
		gameObject.GetComponent<MeshRenderer>().sortingLayerName = SortingLayerName;
		gameObject.GetComponent<MeshRenderer>().sortingOrder = SortingOrder;
	}
}
