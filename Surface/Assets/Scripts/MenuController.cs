using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public void ClickOnPlay()
	{
		SceneManager.LoadScene("LevelSelect 1");
	}

	public void ClickOnExit()
	{
		Application.Quit();
	}
}
