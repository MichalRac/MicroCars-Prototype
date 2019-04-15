using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private bool isPlayerTurn { get; set; }  //Player turn: moment when he can Aim
    
    public AimButtonBehaviour aimButtonBehaviour;
    public CarPhysics carPhysics;
    public UISceneControler uiSceneControler;

    void Start()
    {
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
