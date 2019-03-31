using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private bool isPlayerTurn;              //Player turn: moment when he can Aim

    public AimButtonBehaviour aimButtonBehaviour;
    public CarPhysics carPhysics;

    void Start()
    {
        isPlayerTurn = true;
        StartAimingTurn();
    }

    public void StartAimingTurn()
    {

        Debug.Log("Starting turn");

        isPlayerTurn = true;
        aimButtonBehaviour.showAimButton();

    }

}
