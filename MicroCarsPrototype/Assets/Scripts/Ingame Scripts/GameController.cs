using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private bool isPlayerTurn { get; set; }  //Player turn: moment when he can Aim
    private const int MaxPlayerNumber = 4;

    public GameObject[] levels;
    private GameObject currentLevel;
    private int currentLevelNumber;

    public GameObject[] cars = new GameObject[MaxPlayerNumber];
    public GameObject[] startingPosition = new GameObject[MaxPlayerNumber];
    public float[] rotation = new float[MaxPlayerNumber];

    public AimButtonBehaviour aimButtonBehaviour;
    public CarPhysics carPhysics;
    public UISceneControler uiSceneControler;


    void Start()
    {
        currentLevelNumber = 0;
        currentLevel = Instantiate(levels[0]) as GameObject;
        
        setupLevel();
    }


    public void setupLevel()
    {
        findStartPos();
        for (int i = 0; i < startingPosition.Length; i++)
        {
            if (cars[i] == null)
                break;


            Quaternion carStartingRotation = Quaternion.Euler(0.0f, 0.0f, rotation[i]);

            cars[i].transform.position = startingPosition[i].transform.position;
            cars[i].transform.rotation = carStartingRotation;
        }

        switchTurnState(true);
        StartAimingTurn();
    }

    private void findStartPos() // There should be a better way to do this than by searching the level gameObject by name I think.
    {
        Transform[] startPosTransforms = currentLevel.GetComponentsInChildren<Transform>();
        foreach (Transform p in startPosTransforms)
        {
            if (p.gameObject.name == "start_pos")
            {
                startingPosition[0] = p.gameObject;
                break;
            }
        }
    }


    public void StartAimingTurn()
    {
        switchTurnState(true);
        aimButtonBehaviour.showAimButton();
    }

    public void switchTurnState(bool target)
    {
        isPlayerTurn = target;
        uiSceneControler.switchTurnInfo(isPlayerTurn);
    }

    public void addOneTryCount()
    {
        uiSceneControler.addOneTry();
    }

    public bool getIsPlayerTurn()
    {
        return isPlayerTurn;
    }

    public void nextLevel()
    {
        Destroy(currentLevel.gameObject);

        if (++currentLevelNumber == levels.Length)
            currentLevelNumber = 0;

        currentLevel = Instantiate(levels[currentLevelNumber]) as GameObject;
        setupLevel();
    }

    
}
