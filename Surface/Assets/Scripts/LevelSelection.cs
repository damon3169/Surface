using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnPushTwoPlayer()
	{
		PlayerPrefs.SetInt("numberCase",10);
		PlayerPrefs.SetInt("numberPlayer", 2);
		SceneManager.LoadScene("Play");
	}

	public void OnPushThreePlayer()
	{
		PlayerPrefs.SetInt("numberCase", 10);
		PlayerPrefs.SetInt("numberPlayer", 3);
		SceneManager.LoadScene("Play");
	}

	public void OnPushTeleport()
	{
		PlayerPrefs.SetInt("numberCase", 14);
		PlayerPrefs.SetInt("numberPlayer", 3);
		SceneManager.LoadScene("Play");
	}
}
