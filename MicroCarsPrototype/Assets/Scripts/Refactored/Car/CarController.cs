using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnAimFinishedCallback();
public delegate void OnMovementFinishedCallback();

[RequireComponent(typeof(CarPhysicsRoot))]
[RequireComponent(typeof(AimingRoot))]
[RequireComponent(typeof(CarStates))]
public class CarController : MonoBehaviour
{
    private CarPhysicsRoot carPhysics;
    private AimingRoot aiming;
    private CarStates states;

    OnAimFinishedCallback onAimFinishedCallback;
    OnMovementFinishedCallback onMovementFinishedCallback;
    OnTurnFinishedCallback onTurnFinishedCallbackReference;

    void Start()
    {
        carPhysics = GetComponent<CarPhysicsRoot>();
        aiming = GetComponent<AimingRoot>();
        states = GetComponent<CarStates>();

        onMovementFinishedCallback += OnMovementFinished;
        GetComponent<CarPhysicsRoot>().OnMovementFinishedCallbackReference = onMovementFinishedCallback;
    }

    public void StartCarTurn(OnTurnFinishedCallback onTurnFinishedCallback)
    {
        onTurnFinishedCallbackReference = onTurnFinishedCallback;
        states.IsTurn = true;

        Debug.Log("AimingStarted");
        aiming.AimStart();
    }

    public void OnMovementFinished()
    {
        Debug.Log("Movement Finished");
        FinishCarTurn();
    }

    public void FinishCarTurn()
    {
        onTurnFinishedCallbackReference();
    }

}
