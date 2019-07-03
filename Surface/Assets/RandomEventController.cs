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
			Instantiate(randomEventList[rand]);
		}
	}
}
