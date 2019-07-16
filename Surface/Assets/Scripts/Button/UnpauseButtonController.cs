using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpauseButtonController : ButtonController
{
	protected override void OnMouseDown()
	{
		GameObject.Find("GameController").GetComponent<GameController>().isInPause = false;
		GameObject.Destroy(this.transform.parent.gameObject);
	}
}
