using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    public float timeStart;
    private float output;
    private bool timerActive;

    // Start is called before the first frame update
    void Start()
    {
        output = timeStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            output += Time.deltaTime;
        }
    }

    public float getTime()
    {
        return output;
    }

    public bool isRunning()
    {
        return timerActive;
    }

    public void start()
    {
        timerActive = true;
    }

    public void stop()
    {
        timerActive = false;
    }

    public void restart()
    {
        timerActive = true;
        output = 0f;
    }

    public void reset()
    {
        timerActive = false;
        output = 0f;
    }
}
