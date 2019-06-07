using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTurnFinishedCallback();

public class GameController : MonoBehaviour
{
    //Initializing the class
    #region
    [SerializeField] private GameObject[] _players;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private MainUIBehaviour _mainUIBehaviour;
    [SerializeField] private CameraController mainCameraController;
    [SerializeField] private DigitalRubyShared.TouchBehaviour _touchBehaviour;

    private int _currentLevelId = 0;
    private GameObject _currentLevel;
    private GameObject _currentPlayer;
    private bool _placeholderAllPlayersFinished = false;

    OnTurnFinishedCallback onTurnFinishedCallback;

    public GameObject CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }

    private void Awake()
    {
        _touchBehaviour = gameObject.GetComponent<DigitalRubyShared.TouchBehaviour>();
        mainCameraController = gameObject.GetComponent<CameraController>();

        _currentLevel = Instantiate(_levels[_currentLevelId]);
        _currentPlayer = Instantiate(_players[0]) as GameObject;
        MovePlayerToStartPos(_currentPlayer);

        onTurnFinishedCallback += StartIdCarTurn;
    }

    //Starting the level loop
    private void Start()
    {
        mainCameraController.switchTarget(_currentPlayer);
        _touchBehaviour.SetupTouchForPlayer(_currentPlayer);
        StartIdCarTurn();
    }
    #endregion

    //Main level loop and finishing the game
    #region

    //Calling next available player (for now only one player, but it will be easy to expand)
    public void StartIdCarTurn() //For now looping only playerid[0] turn
    {
        StartCoroutine(FinishAllOperationsAndStartNextTurn());
    }

    public IEnumerator FinishAllOperationsAndStartNextTurn()
    {
        yield return null;
        if (_currentPlayer.GetComponent<CarController>().States.IsLevelFinished)
            OnLevelFinished();
        else
            _currentPlayer.GetComponent<CarController>().StartCarTurn(onTurnFinishedCallback);
    }

    public void OnLevelFinished()
    {
        //Level completed popup show
        _mainUIBehaviour.SetActiveLevelCompletedPupup(true);
    }

    #endregion

    #region
    public void NextLevel()
    {
        Destroy(_currentLevel);

        if (_currentLevelId == _levels.Length - 1)
            _currentLevelId = 0;
        else
            _currentLevelId++;

        _currentLevel = Instantiate(_levels[_currentLevelId]);
        _currentPlayer.GetComponent<CarStates>().IsLevelFinished = false;
        MovePlayerToStartPos(_currentPlayer);
        StartCoroutine(FinishAllOperationsAndStartNextTurn());
    }

    public void MovePlayerToStartPos(GameObject player)
    {
        player.transform.position = GetStartPosition();
    }

    public Vector3 GetStartPosition()
    {
        return _currentLevel.transform.Find("Points/start_pos").transform.position;
    }
    #endregion
}
