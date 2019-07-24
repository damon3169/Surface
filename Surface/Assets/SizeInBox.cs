using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SizeInBox : MonoBehaviour
{
	float widthCollider;
	float heightCollider;
public 	List<GameObject> Childs;
	public void Resize()
	{
		Childs = new List<GameObject>();
		for (int i = 0; i<this.transform.childCount; i++)
		{
			Childs.Add(this.transform.GetChild(i).gameObject);
		}
		Childs = Childs.OrderBy(x => x.transform.position.x).ToList();
		float middle = (Childs[0].transform.position.x + Childs[Childs.Count - 1].transform.position.x) / 2;
		Vector3 distanceMiddle = new Vector3(this.transform.position.x - middle, 0, 0);
		
		foreach (GameObject child in Childs)
		{
			child.transform.position += distanceMiddle;
		}

		Childs = Childs.OrderBy(y => y.transform.position.y).ToList();
		middle = (Childs[0].transform.position.y + Childs[Childs.Count - 1].transform.position.y) / 2;
		distanceMiddle = new Vector3(0 , this.transform.position.y - middle, 0);

		foreach (GameObject child in Childs)
		{
			child.transform.position += distanceMiddle;
		}


		float widthTotalx = (Childs[0].transform.position.x - Childs[0].transform.localScale.x) - (Childs[Childs.Count - 1].transform.position.x + Childs[0].transform.localScale.x);
		float widthTotaly = (Childs[Childs.Count - 1].transform.position.y + Childs[0].transform.localScale.y) - (Childs[0].transform.position.y - Childs[0].transform.localScale.y);
		float newSize;
		if (widthTotalx > widthTotaly)
		{
			newSize = widthCollider / widthTotalx;
		}
		else
		{
			newSize = heightCollider / widthTotaly;
		}
		//this.transform.localScale = this.transform.localScale * Mathf.Abs(newSize);
	}
}
