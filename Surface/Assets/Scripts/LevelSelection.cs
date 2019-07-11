using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelection : MonoBehaviour
{
	int numPlayer = 1;
	public int minPlayer;
	public int maxPlayer;
	public Text text;
	// Start is called before the first frame update

	private void Start()
	{
		numPlayer = minPlayer;
	}

	private void Update()
	{
		text.text = numPlayer.ToString();
	}

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

	public void onPushPlus()
	{
		if (numPlayer < maxPlayer)
		{
			numPlayer++;
		}
	}

	public void onPushMinus()
	{
		if (numPlayer > minPlayer)
		{
			numPlayer--;
		}
	}

	public void OnPushMapSelection()
	{
		PlayerPrefs.SetInt("numberPlayer", numPlayer);
		SceneManager.LoadScene("MapSelection");
	}
}
