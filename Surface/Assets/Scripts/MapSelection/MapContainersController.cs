using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapContainersController : MonoBehaviour
{
	private float timerMouseDown;

	private void OnMouseUp()
	{
		if (this.transform.parent.GetComponent<MapSelection>().m_Event.mousePosition == this.transform.parent.GetComponent<MapSelection>().StartPos)
		{
			this.transform.GetChild(0).GetComponent<MapManager>().openPopup();
		}
	}
}
