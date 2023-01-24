using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public float slowdownFactor = 0.2f;


    public void makeSlow()
    {
        Time.timeScale = slowdownFactor;
    }

    public void makeFast()
    {
        Time.timeScale = 1f;
    }
}
