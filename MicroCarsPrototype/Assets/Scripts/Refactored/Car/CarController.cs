﻿using System.Collections;
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
    public CarStates States { get => states; set => states = value; }

    [SerializeField]
    private GameObject car;
    [SerializeField]
    private GameObject ghostCar;

    OnAimFinishedCallback onAimFinishedCallback;
    OnMovementFinishedCallback onMovementFinishedCallback;
    OnTurnFinishedCallback onTurnFinishedCallbackReference;


    private void Awake()
    {
        carPhysics = GetComponent<CarPhysicsRoot>();
        aiming = GetComponent<AimingRoot>();
        States = GetComponent<CarStates>();
        Debug.Assert(carPhysics, $"{typeof(CarPhysicsRoot)} is null");
        Debug.Assert(aiming, $"{typeof(AimingRoot)} is null");
        Debug.Assert(States, $"{typeof(CarStates)} is null");
    }

    void Start()
    {
        onMovementFinishedCallback += OnMovementFinished;
        carPhysics.OnMovementFinishedCallbackReference = onMovementFinishedCallback;

        car = Instantiate(car, transform) as GameObject;
        ghostCar = Instantiate(ghostCar, transform) as GameObject;

        GameObject[] objectsForAiming = { car, ghostCar };
        GetComponent<AimingPositioning>().injectPlayerChildObjects(objectsForAiming);
    }

    public void StartCarTurn(OnTurnFinishedCallback onTurnFinishedCallback)
    {
        onTurnFinishedCallbackReference = onTurnFinishedCallback;
        States.IsTurn = true;

        aiming.AimTurnStart();
    }

    public void OnMovementFinished()
    {
        FinishCarTurn();
    }

    public void FinishCarTurn()
    {
        States.IsTurn = false;
        onTurnFinishedCallbackReference();
    }
}
