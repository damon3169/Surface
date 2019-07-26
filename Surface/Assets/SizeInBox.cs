using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SizeInBox : MonoBehaviour
{
	float widthCollider;
	float heightCollider;

	public void Awake()
	{
		widthCollider = GetComponent<BoxCollider2D>().size.x;
		heightCollider = GetComponent<BoxCollider2D>().size.y;
	}

	public 	List<GameObject> Childs;
	public void Resize()
	{
		Childs = new List<GameObject>();
		for (int i = 0; i<this.transform.childCount; i++)
		{
			if (this.transform.GetChild(i).tag != "Sprite")
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

	public void ResizeBetter()
	{
		Childs = new List<GameObject>();
		for (int i = 0; i < this.transform.GetChild(1).childCount; i++)
		{
				Childs.Add(this.transform.GetChild(1).GetChild(i).gameObject);
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
		distanceMiddle = new Vector3(0, this.transform.position.y - middle, 0);

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
		this.transform.GetChild(1).transform.localScale = this.transform.GetChild(1).transform.localScale * Mathf.Abs(newSize);
	}
}
