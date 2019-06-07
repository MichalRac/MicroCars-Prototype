using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO make it work :/

public delegate void TimeManagerDelegate();
public class TimeManager : MonoBehaviour
{
    public void doNextFrame(TimeManagerDelegate method)
    {
        StartCoroutine(doNextFrameCoroutine(method));
    }
        
    private IEnumerator doNextFrameCoroutine(TimeManagerDelegate method)
    {
        yield return null;
        method();
    }
}
