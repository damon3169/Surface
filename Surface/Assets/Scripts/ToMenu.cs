using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{

    public void ClickOnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ClickOnExit()
    {
        Application.Quit();
    }
}

