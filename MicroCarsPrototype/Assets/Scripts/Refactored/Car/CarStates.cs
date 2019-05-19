using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStates : MonoBehaviour
{
    private bool isMoving;
    private bool isAiming;
    private bool isTurn;

    public bool IsMoving
    {
        get
        {
            return this.isMoving;
        }

        set
        {
            this.isMoving = value;
            Debug.Log(string.Format("The moving state changed to: {0}", isMoving));
        }
    }

    public bool IsAiming
    {
        get
        {
            return isAiming;
        }

        set
        {
            if (IsMoving)
                return;
            isAiming = value;
            Debug.Log("Aiming state changed to: " + IsAiming);
        }
    }

    public bool IsTurn
    {
        get
        {
            return isTurn;
        }

        set
        {
            isTurn = value;
        }
    }
}
