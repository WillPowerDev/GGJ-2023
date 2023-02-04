using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Timer
{
    [SerializeField] float duration;
    [SerializeField] Action onComplete;

    bool active;
    float timer;
    bool loop;

    public Timer(float duration, Action onComplete, bool loop = false)
    {
        this.duration = duration;
        this.onComplete = onComplete;
        this.loop = loop;
    }

    public void Begin()
    {
        Reset();
        Start();
    }

    public void Start()
    {
        Debug.Log("Timer.cs  Starting at " + timer);
        active = true;
    }

    public void Tick(float deltaTime)
    {
        if (!active) return; 

        if (timer > duration)
        {
            onComplete?.Invoke();
            if (loop)
            {
                timer = 0;
                return;
            }
            active = false; 
        } 
        else 
        {
            timer += deltaTime;
        }
    }

    public void Stop()
    {
        Debug.Log("Timer.cs  Pausing at " + timer);
        active = false;
    }

    public void Reset()
    {
        timer = 0;
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    public void SetOnComplete(Action onComplete)
    {
        this.onComplete = onComplete;
    }

    public float GetDuration()
    {
        return duration;
    }

    public float GetTime()
    {
        return timer;
    }
}
