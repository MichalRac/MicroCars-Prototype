using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StartNextPlayerTurnCallback();

public class GameController : MonoBehaviour
{

    public GameObject[] players;
    public GameObject[] levels;

    private GameObject currentLevelId;
    private int currentPlayerId = 0;
    private bool placeholderAllPlayersFinished = false;

    StartNextPlayerTurnCallback startNextPlayerTurnCallback;

    private void Start()
    {
        startIdCarTurn(0);
        startNextPlayerTurnCallback = () => startIdCarTurn(0); //For now looping playerid[0] turn
    }

    public void startIdCarTurn(int carID)
    {
        players[carID].GetComponent<CarController>().StartCarTurn(startNextPlayerTurnCallback);
    }
}
