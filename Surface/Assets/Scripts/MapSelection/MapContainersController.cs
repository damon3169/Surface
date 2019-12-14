using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapContainersController : MonoBehaviour
{
	private float timerMouseDown;
	private RaycastHit2D hit = new RaycastHit2D();
	public int mapIndex;

	private void OnMouseUp()
	{
		if (this.transform.parent.GetComponent<MapSelection>().m_Event.mousePosition == this.transform.parent.GetComponent<MapSelection>(). StartPos2 && !this.transform.parent.GetComponent<MapSelection>().isPopupOpen)
		{
			this.transform.GetChild(0).GetComponent<MapManager>().openPopup();
		}
	}

	void FixedUpdate()
	{
		//Update the Text on the screen depending on current TouchPhase, and the current direction vector

		// Track a single touch as a direction control.
		if (Input.touchCount > 0 && !this.transform.parent.GetComponent<MapSelection>().isPopupOpen && mapIndex == this.transform.parent.GetComponent<MapSelection>().mapFocused)
		{
			Touch touch = Input.GetTouch(0);
			// Handle finger movements based on TouchPhase
			switch (touch.phase)
			{
				case TouchPhase.Ended:

					Vector2 ray = Camera.main.ScreenToWorldPoint(touch.position);
					hit = Physics2D.Raycast(ray, ray.normalized, 1);
					if (hit.transform.GetComponent<MapContainersController>() != null)
					{
						if (this.transform.parent.GetComponent<MapSelection>().delta.magnitude < this.transform.parent.GetComponent<MapSelection>().minMovement)
						{
							this.transform.GetChild(0).GetComponent<MapManager>().openPopup();
						}
					}


					break;
			}
		}
	}
}
