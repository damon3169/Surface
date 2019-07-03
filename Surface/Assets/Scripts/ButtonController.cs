using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
	float timerForDoubleClick = 0.0f;
	float delay = 0.3f;
	bool isDoubleClick = false;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		/*if (Input.touchCount > 0)
		{

			if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				GameObject.Destroy(this.transform.parent.gameObject);
			}
		}*/
	}


	void OnMouseOver()
	{
		if (Input.GetButtonDown("Fire1") && isDoubleClick == false)
		{
			Debug.Log("ok");
			isDoubleClick = true;
		}

	}

	void OnMouseDown()
	{
		GameObject.Destroy(this.transform.parent.gameObject);
	}
}
