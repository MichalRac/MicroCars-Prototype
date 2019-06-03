using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStates : MonoBehaviour
{
    private bool _isLevelFinished;
    private bool _isMoving;
    private bool _isAiming;
    private bool _isTurn;
    private int _turnsDone;


    public bool IsLevelFinished {
        get => _isLevelFinished;
        set
        {
            _isLevelFinished = value;
            if(_isLevelFinished)
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
            if(_isTurn)
                Debug.Log("Turn Started");
            if(!_isTurn)
                Debug.Log("Turn finished");
        }
    }
}
