using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimingPositioning))]
public class AimingRoot : MonoBehaviour
{
    

    private AimingPositioning positioning;

    private void Start()
    {
        positioning = GetComponent<AimingPositioning>();
    }

    public void OnAimHold()
    {
        positioning.MoveAimToPointer();
    }

    public void OnAimRelease()
    {

    }

    
}
