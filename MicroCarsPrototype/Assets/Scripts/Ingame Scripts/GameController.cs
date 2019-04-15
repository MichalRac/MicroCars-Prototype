using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private bool isPlayerTurn { get; set; }  //Player turn: moment when he can Aim
    private const int MaxPlayerNumber = 4;
   
    
    public GameObject[] cars = new GameObject[MaxPlayerNumber];
    public GameObject[] startingPosition = new GameObject[MaxPlayerNumber];
    public float[] rotation = new float[MaxPlayerNumber];

    public AimButtonBehaviour aimButtonBehaviour;
    public CarPhysics carPhysics;
    public UISceneControler uiSceneControler;


    void Start()
    {

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

    public void StartAimingTurn()
    {
        switchTurnState(true);
        aimButtonBehaviour.showAimButton();
    }

    public void addOneTryCount()
    {
        uiSceneControler.addOneTry();
    }

    public void switchTurnState(bool target)
    {
        isPlayerTurn = target;
        uiSceneControler.switchTurnInfo(isPlayerTurn);
    }

    public bool getIsPlayerTurn()
    {
        return isPlayerTurn;
    }

    
}
