using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIBehaviour : MonoBehaviour
{
    private GameObject LevelCompletedPopup;

    private void Awake()
    {
        LevelCompletedPopup = transform.Find("PopupLevelCompleted").gameObject;
    }

    public void SetActiveLevelCompletedPupup(bool boolean)
    {
        if (LevelCompletedPopup)
            LevelCompletedPopup.SetActive(boolean);
        else
            Debug.Log("ERROR: LevelCompletedPopup not found");
    }
}
