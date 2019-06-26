using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RandomEvent : MonoBehaviour
{
    public string popupText;
    public bool isUsingCounter;
    public AudioClip monster;

    [System.Serializable]
    public class audioClips
    {
        public int num;
        public AudioClip audio;
    }

    [SerializeField]
    public List<audioClips> audioClipList;


    public void launchRandomEvent()
    {
        //super;
        GameObject gameController = GameObject.Find("GameController");
        GameController gameControllerComp = GameObject.Find("GameController").GetComponent<GameController>();

        gameController.GetComponent<AudioSource>().clip = monster;
        gameController.GetComponent<AudioSource>().Play();
        gameControllerComp.popupText.text = this.popupText;
        gameControllerComp.isEventOn = true;
        gameControllerComp.popup.SetActive(true);
        if (isUsingCounter)
        {
            gameControllerComp.timerText.color = Color.red;
            gameControllerComp.timerEventBegin = Time.time;
            gameControllerComp.audioClipsList = audioClipList;
        }
        gameControllerComp.timeLastEvent = Time.time;
    }
}
