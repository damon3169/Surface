using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveDonjonDraw : MonoBehaviour
{
	void OnMouseDown()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GoBackToPortionSelection();
		this.transform.parent.GetComponent<DrawerPanel>().DrawerOut(this.transform.parent.GetComponent<DrawerPanel>().hidingPosition, true, false);
	}
}
