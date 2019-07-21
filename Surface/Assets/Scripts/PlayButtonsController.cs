using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayButtonsController : MonoBehaviour
{
	public Image pauseButton;

	public Sprite pauseSprite;

	public Sprite playSprite;

	public TextMeshProUGUI textRandomNumber;

	public bool isDiceMoving = false;
	public float DiceTimer = 5f;
	public MapManager map;

	private int lastValue = 0;
	float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the
	// filtered value will converge towards current input sample (and vice versa).
	float lowPassKernelWidthInSeconds = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation,
	// or at least according to Brady! ;)
	float shakeDetectionThreshold = 2.0f;

	float lowPassFilterFactor;
	Vector3 lowPassValue;

	private void Start()
	{
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
	}

	void Update()
	{
		if (GameObject.Find("GameController").GetComponent<GameController>().isInPause)
		{
			pauseButton.sprite = playSprite;
		}
		else
		{
			pauseButton.sprite = pauseSprite;
		}
		if (DiceTimer <= 0f)
		{
			isDiceMoving = false;
		}

		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;

		if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
		{
			if (!isDiceMoving)
			{
				System.Random rand = new System.Random(); // This seems to repeat itself and is slow!
				int randomNum = rand.Next(1, PlayerPrefs.GetInt("numberCase") + 1);
				isDiceMoving = true;
				DiceTimer = 5;
				map.Tiles[lastValue].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
				map.Tiles[randomNum - 1].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
				lastValue = randomNum - 1;
				if (randomNum < 10)
					textRandomNumber.text = "0" + randomNum.ToString();
				else textRandomNumber.text = randomNum.ToString();
				StartCoroutine(DiceMoving());
			}
		}
	}

	public void ClickOnPause()
	{
		GameController GameController = GameObject.Find("GameController").GetComponent<GameController>();
		if (GameController.TimerPause >0)
		{
			GameObject.Find("GameController").GetComponent<GameController>().isInPause = !GameObject.Find("GameController").GetComponent<GameController>().isInPause;
		}
		else
		{

		}
	}

	public void ClickOnMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void ClickOnDice()
	{
		if (!isDiceMoving)
		{
			System.Random rand = new System.Random(); // This seems to repeat itself and is slow!
													  //int randomNum = rand.Next(1, PlayerPrefs.GetInt("numberCase") + 1);
			int randomNum = rand.Next(1, PlayerPrefs.GetInt("numberCase") + 1);
			if (PlayerPrefs.GetInt("numberPlayer") == 2)
			{
				if (randomNum == 8)
					textRandomNumber.text = "13";
				if (randomNum == 9)
					textRandomNumber.text = "14";
			}
			isDiceMoving = true;
			DiceTimer = 5;
			if (randomNum < 10)
				textRandomNumber.text = "0" + randomNum.ToString();
			else textRandomNumber.text = randomNum.ToString();
			StartCoroutine(DiceMoving());
		}

	}

	public void onClickSound()
	{
		if (this.GetComponent<AudioSource>().isPlaying)
		{
			this.GetComponent<AudioSource>().Stop();
		}
		else
		{
			this.GetComponent<AudioSource>().Play();
		}
	}

	public IEnumerator DiceMoving()
	{
		while (DiceTimer > 0f)
		{
			yield return new WaitForSeconds(0.1f);
			System.Random rand = new System.Random(); // This seems to repeat itself and is slow!
			int randomNum = rand.Next(1, PlayerPrefs.GetInt("numberCase") + 1);
			isDiceMoving = true;
			DiceTimer -= 1;
			//map.Tiles[lastValue].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
			//map.Tiles[randomNum ].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
			lastValue = randomNum;

			if (randomNum < 10)
				textRandomNumber.text = "0" + randomNum.ToString();
			else textRandomNumber.text = randomNum.ToString();
			if (PlayerPrefs.GetInt("numberPlayer") == 2)
			{
				if (randomNum == 8)
					textRandomNumber.text = "13";
				if (randomNum == 9)
					textRandomNumber.text = "14";
			}
		}

	}

	public void PopUpCloseButton()
	{
		GameObject.Find("popup").SetActive(false);
	}
}
