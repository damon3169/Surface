using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInBackground : MonoBehaviour
{
    AndroidJavaClass unityClass;
    AndroidJavaObject unityActivity;
    AndroidJavaClass customClass;
    public float counter = 0;

    void Start()
    {
        //Replace with your full package name
        sendActivityReference("com.example.surfacelib");

        //Now, start service
        startService();
    }

    void sendActivityReference(string packageName)
    {
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        customClass = new AndroidJavaClass(packageName);
        customClass.CallStatic("receiveActivityInstance", unityActivity);
    }

    void startService()
    {
        counter =customClass.Call<float>("StartCheckerService", counter);
    }
}
