using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
	bool isDoubleClick = false;



	void OnMouseOver()
	{
		if (Input.GetButtonDown("Fire1") && isDoubleClick == false)
		{
			isDoubleClick = true;
		}

	}

	protected virtual void OnMouseDown()
	{
		GameObject.Destroy(this.transform.parent.gameObject);
	}
}
