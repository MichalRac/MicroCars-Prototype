using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTurnFinishedCallback();

public class GameController : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] levels;

    private GameObject currentLevelId;
    private int currentPlayerId = 0;
    private bool placeholderAllPlayersFinished = false;

    OnTurnFinishedCallback onTurnFinishedCallback;

    private void Start()
    {
        onTurnFinishedCallback += startIdCarTurn;
        startIdCarTurn();
    }

    public void startIdCarTurn() //For now looping playerid[0] turn
    {
        players[0].GetComponent<CarController>().StartCarTurn(onTurnFinishedCallback);
    }
}
