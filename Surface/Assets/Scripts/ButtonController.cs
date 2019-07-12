using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

	protected virtual void OnMouseDown()
	{
		GameObject.Destroy(this.transform.parent.gameObject);
	}
}
