using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
//https://answers.unity.com/questions/904082/timer-run-in-background.html
//http://jeanmeyblum.weebly.com/scripts--tutorials/communication-between-an-android-app-and-unity
public class GameController : MonoBehaviour
{
    public List<float[]> phaseTime;

    private int phase = 0;

    public TextMeshProUGUI textTimer;

    public bool isInPause = false;

    public bool isDiceMoving = false;

    //public GameObject map;

    private float xMax = 0;
    private float yMax = 0;
    private float xMin = 0;
    private float yMin = 0;
    private GameObject xMaxObject;
    private GameObject yMaxObject;
    private GameObject xMinObject;
    private GameObject yMinObject;
    private float yDist;
    private float xDist;
    public AudioClip alarm;

    public TextMeshProUGUI popupText;
    public TextMeshProUGUI timerText;
    private float timerEventDuration = 30f;
    public float timerEventBegin = 0f;
    public GameObject popup;
    public AudioClip monster;
    private int frameCount = 0;
    public bool isEventOn = false;
    public AudioClip radioClip;
    private float timeBetweenEvents = 0f;
    public float timeLastEvent = 0f;
    public List<RandomEvent.audioClips> audioClipsList;
    private int audioCounter = 0;
    private bool isThereAudioToPlay = true;
    public RunInBackground background;
	RandomEventController RandomeEventController;
	// Start is called before the first frame update
	void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        phaseTime = new List<float[]>();
        phaseTime.Add(new float[] { 3, 0 });
        phaseTime.Add(new float[] { 5, 0 });
        phaseTime.Add(new float[] { 7, 0 });
        if (phaseTime[phase][1] >= 10)
            textTimer.text = "0" + phaseTime[phase][0].ToString() + ":" + phaseTime[phase][1].ToString().Substring(0, 2);
        else { textTimer.text = "0" + phaseTime[phase][0].ToString() + ":0" + phaseTime[phase][1].ToString().Substring(0, 1); }
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == phaseTime.Count)
        {
            SceneManager.LoadScene("Game Over");
        }

        if (!isInPause)
        {

            frameCount++;
            if (frameCount % 10 == 0)
            {
                int rand = Random.Range(0, 100);
                if (rand == 1)
                {
                    if (timeBetweenEvents < Time.time - timeLastEvent)
                    {
                        timeBetweenEvents = 180f;
						RandomeEventController.launchRandomEvent();
                    }
                }

                if (isEventOn && isThereAudioToPlay && audioClipsList.Count != 0 && audioClipsList[audioCounter].num < Time.time - timerEventBegin )
                {
                    this.GetComponent<AudioSource>().clip = audioClipsList[audioCounter].audio;
                    this.GetComponent<AudioSource>().Play();
                    if (audioCounter == audioClipsList.Count - 1)
                        isThereAudioToPlay = false;
                    else
                        audioCounter++;
                }

                if (timerEventDuration < Time.time - timerEventBegin && isEventOn)
                {
                    isEventOn = false;
                    this.GetComponent<AudioSource>().clip = radioClip;
                    this.GetComponent<AudioSource>().Play();
                    timerText.color = Color.white;
                    audioCounter = 0;
                    isThereAudioToPlay = true;
                }

              
            }

            phaseTime[phase][1] -= Time.deltaTime;

            if (phaseTime[phase][1] <= 0)
            {
                phaseTime[phase][0] -= 1;
                if (phaseTime[phase][0] < 0)
                {
                    phase++;
                    this.GetComponent<AudioSource>().clip = alarm;
                    this.GetComponent<AudioSource>().Play();
                    //isInPause = true; 
                    // play alarme
                }
                else
                {
                    phaseTime[phase][1] = 60;
                    phaseTime[phase][1] = phaseTime[phase][1] - Time.deltaTime;
                }

            }
            else
            {
                if (phaseTime[phase][1] >= 10)
					textTimer.text = "0" + phaseTime[phase][0].ToString() + ":" + phaseTime[phase][1].ToString().Substring(0, 2);
                else {
					 textTimer.text = "0" + phaseTime[phase][0].ToString() + ":0" + phaseTime[phase][1].ToString().Substring(0, 1);
                }
            }
        }
    }

}
