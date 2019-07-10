using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonRandomEffect : MonoBehaviour
{
	void OnMouseDown()
	{
		this.transform.parent.GetComponent<DrawerPanel>().DrawerOut(this.transform.parent.GetComponent<DrawerPanel>().hidingPosition, true);
	}
}
