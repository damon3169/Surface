using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPopupMapController : ButtonController
{
	private void Start()
	{
		GameObject.FindGameObjectWithTag("MapSelection").GetComponent<MapSelection>().isPopupOpen = true;
		Debug.Log(GameObject.FindGameObjectWithTag("MapSelection").GetComponent<MapSelection>().isPopupOpen);
	}

	protected override void OnMouseDown()
	{
		GameObject.FindGameObjectWithTag("MapSelection").GetComponent<MapSelection>().isPopupOpen = false;
		GameObject.Destroy(this.transform.parent.gameObject);
	}
}
