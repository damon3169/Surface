using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLauncher : ButtonController
{

	protected override void OnMouseDown()
	{
		Debug.Log("test");
		PlayerPrefs.SetInt("numberCase", transform.parent.parent.childCount - 1);
		SceneManager.LoadScene("Play 2");
	}
}
