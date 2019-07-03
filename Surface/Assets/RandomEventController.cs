﻿using System.Collections;
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
			RandomEvent.transform.parent= this.transform;
			RandomEvent.transform.localScale = new Vector3(1, 1, 1);
		}
	}
}
