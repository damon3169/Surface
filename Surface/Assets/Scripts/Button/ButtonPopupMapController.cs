using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPopupMapController : ButtonController
{
	private void Start()
	{
		GameObject.FindGameObjectWithTag("MapSelection").GetComponent<MapSelection>().isPopupOpen = true;
		this.transform.parent.parent.parent.GetComponent<Collider2D>().enabled = false;
		DestroyImmediate(this.transform.parent.parent.parent.GetComponent<Rigidbody2D>());

		Debug.Log(GameObject.FindGameObjectWithTag("MapSelection").GetComponent<MapSelection>().isPopupOpen);
	}

	protected override void OnMouseDown()
	{
		GameObject.FindGameObjectWithTag("MapSelection").GetComponent<MapSelection>().isPopupOpen = false;
		this.transform.parent.parent.parent.GetComponent<Collider2D>().enabled = true;
		GameObject.Destroy(this.transform.parent.gameObject);
		gameObject.AddComponent<Rigidbody2D>();
		this.transform.parent.parent.parent.GetComponent<Rigidbody2D>();
	}
}
