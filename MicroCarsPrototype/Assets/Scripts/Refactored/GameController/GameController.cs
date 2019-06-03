using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTurnFinishedCallback();

public class GameController : MonoBehaviour
{
    //Initializing the class
    #region
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private MainUIBehaviour _mainUIBehaviour;

    private GameObject currentLevelId;
    private int currentPlayerId = 0;
    private bool placeholderAllPlayersFinished = false;

    OnTurnFinishedCallback onTurnFinishedCallback;

    //Starting the level loop
    private void Start()
    {
        onTurnFinishedCallback += StartIdCarTurn;
        StartIdCarTurn();
    }
    #endregion

    //Main level loop and finishing the game
    #region
    //Calling next available player (for now only one player, but it will be easy to expand)
    public void StartIdCarTurn() //For now looping playerid[0] turn
    {
        if (!players[0].GetComponent<CarController>().States.IsLevelFinished)
            players[0].GetComponent<CarController>().StartCarTurn(onTurnFinishedCallback);
        else
            OnLevelFinished();
    }

    public void OnLevelFinished()
    {
        //Level completed popup show
        _mainUIBehaviour.SetActiveLevelCompletedPupup(true);
    }
    public IEnumerator StartNextCarTurn()
    {
        yield return null;
        if (players[0].GetComponent<CarController>().States.IsLevelFinished)
            OnLevelFinished();
        else
            players[0].GetComponent<CarController>().StartCarTurn(onTurnFinishedCallback);
    }
    #endregion
}
