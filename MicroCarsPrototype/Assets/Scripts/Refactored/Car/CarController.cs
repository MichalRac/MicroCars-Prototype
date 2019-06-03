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
    //Setting up the class
    #region
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
    #endregion

    //Starting, finishing and controlling player's turn
    //Player turn is right now composed of aiming and movement sequences
    #region
        //Called from the GameController when player's turn starts
        public void StartCarTurn(OnTurnFinishedCallback onTurnFinishedCallback)
        {
            onTurnFinishedCallbackReference = onTurnFinishedCallback;
            States.IsTurn = true;

            //Starting aiming turn sequence
            aiming.AimTurnStart();
        }

        //Called between finished movement and before ending turn
        public void OnMovementFinished()
        {
            FinishCarTurn();
        }

        //Ending turn either on OnMovementFinished or other reasons (TODO timeout?)
        public void FinishCarTurn()
        {
            States.IsTurn = false;
            onTurnFinishedCallbackReference();
        }
    #endregion

}
