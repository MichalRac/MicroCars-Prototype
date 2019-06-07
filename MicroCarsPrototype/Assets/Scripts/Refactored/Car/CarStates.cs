using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStates : MonoBehaviour
{
    private bool _isLevelFinished = false;
    private bool _isMoving = false;
    private bool _isAiming = false;
    private bool _isTurn = false;
    private bool _hitWall = false;
    private int _turnsDone = 0;

    private CarPhysicsMovement movement;

    private void Awake()
    {
        movement = GetComponent<CarPhysicsMovement>();
        Debug.Assert(movement, $"{typeof(CarPhysicsMovement)} is null");
    }

    public bool IsLevelFinished {
        get => _isLevelFinished;
        set
        {
            _isLevelFinished = value;
            if (_isLevelFinished)
            {
                Debug.Log($"{gameObject.name} has finished the level");
            }
        }
    }
    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            this._isMoving = value;
            Debug.Log(string.Format("The moving state changed to: {0}", _isMoving));
        }
    }

    public bool IsAiming
    {
        get => _isAiming;
        set
        {
            if (IsMoving)
                return;
            _isAiming = value;
            Debug.Log("Aiming state changed to: " + IsAiming);
        }
    }

    public bool IsTurn
    {
        get => _isTurn;
        set
        {
            _isTurn = value;
            if (_isTurn)
                Debug.Log("Turn Started");
            if (!_isTurn)
                Debug.Log("Turn finished");
        }
    }

    public bool HitWall
    {
        get => _hitWall;
        set
        {
            _hitWall = value;
            if (_hitWall == true)
                StartCoroutine(TurnHitWallStateAfterNSeconds(2));
        }
    }

    public IEnumerator TurnHitWallStateAfterNSeconds(int n)
    {
        movement.StopCoroutine("MaintainVelocityRotation");
        yield return new WaitForSeconds(n);
        HitWall = false;

        while (IsMoving && movement.GetAngularVelocity > 5)
            yield return null;

        if(IsMoving)
            movement.StartCoroutine("MaintainVelocityRotation");
    }
}
