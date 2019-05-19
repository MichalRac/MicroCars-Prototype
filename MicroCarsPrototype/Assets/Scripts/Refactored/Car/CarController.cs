using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnAimFinishedCallback();

[RequireComponent(typeof(CarPhysicsRoot))]
[RequireComponent(typeof(AimingRoot))]
[RequireComponent(typeof(CarStates))]
public class CarController : MonoBehaviour
{

    private CarPhysicsRoot carPhysics;
    private AimingRoot aiming;
    private CarStates states;

    StartNextPlayerTurnCallback startNextPlayerTurnCallback;
    public static OnAimFinishedCallback onAimFinished;

    // Start is called before the first frame update
    void Start()
    {
        carPhysics = GetComponent<CarPhysicsRoot>();
        aiming = GetComponent<AimingRoot>();
        states = GetComponent<CarStates>();

        onAimFinished = () => OnMovementFinished();
    }

    public void StartCarTurn(StartNextPlayerTurnCallback passedGameControllerCallback)
    {
        states.IsTurn = true;
        startNextPlayerTurnCallback = passedGameControllerCallback;
        

        Debug.Log("AimingStarted");
        aiming.AimStart(onAimFinished);
    }

    public void OnMovementFinished()
    {
        FinishCarTurn();
    }

    public void FinishCarTurn()
    {
        startNextPlayerTurnCallback();
    }

    public void passGameControllerCallback(StartNextPlayerTurnCallback passedStartNextPlayerTurnCallback)
    {
        startNextPlayerTurnCallback = passedStartNextPlayerTurnCallback;
    }
}
