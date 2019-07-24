using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventController : MonoBehaviour
{
	public List<GameObject> randomEventList;

	public void launchRandomEvent()
	{
		if (randomEventList.Count > 0)
		{
			int rand = Random.Range(0, randomEventList.Count);
			GameObject RandomEvent = Instantiate(randomEventList[rand]);
			RandomEvent.transform.SetParent(this.transform,false);
			for (int i = 0; i< GameObject.FindGameObjectWithTag("Donjon").transform.childCount; i++)
			{
				if(!GameObject.Find("EventSystem").GetComponent<DonjonGenerator>().pStage4.Contains(GameObject.FindGameObjectWithTag("Donjon").transform.GetChild(i).gameObject))
				{
					GameObject.FindGameObjectWithTag("Donjon").transform.GetChild(i).transform.position += new Vector3(0, 3f, 0);
				}
			}
		}
	}
}
