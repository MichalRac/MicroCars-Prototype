using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimingPositioning))]
public class AimingRoot : MonoBehaviour
{
    
    [HideInInspector]
    private AimingPositioning positioning;
    private CarStates states;

    OnAimFinishedCallback onAimFinishedCallbackReference;

    private void Start()
    {
        positioning = GetComponent<AimingPositioning>();
        states = GetComponent<CarStates>();
    }

    public void OnAimHold()
    {
        positioning.MoveAimToPointer();
    }

    public void OnAimRelease()
    {
        positioning.onAimRelease();
        states.IsAiming = false;
    }

    // AimStart
    public void AimStart()
    {
        states.IsAiming = true;
        positioning.ShowAimButton();

        StartCoroutine(aimingLifetime());
        
    }

    //AimDuration
    private IEnumerator aimingLifetime()
    { 
        while(states.IsAiming)
            yield return null;

        AimEnd();

    }
    
    //AimEnd + Callback
    private void AimEnd()
    {
    }

}
