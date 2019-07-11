using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtonController : MonoBehaviour
{
	MapSelection mapSelection;
	public int MapIndex = 0;

	private void Start()
	{
		mapSelection = GameObject.FindGameObjectWithTag("MapSelection").GetComponent<MapSelection>();
	}

	private void OnMouseDown()
	{
		mapSelection.ChangeFocusedMap(MapIndex);
		mapSelection.lerpMap();
	}
}
