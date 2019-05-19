using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimingPositioning))]
public class AimingRoot : MonoBehaviour
{
    
    [HideInInspector]
    private AimingPositioning positioning;
    private CarStates states;

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
    public void AimStart(OnAimFinishedCallback onAimFinishedCallback)
    {
        states.IsAiming = true;
        positioning.ShowAimButton();

        StartCoroutine(aimingLifetime(onAimFinishedCallback));
        
    }

    //AimDuration
    private IEnumerator aimingLifetime(OnAimFinishedCallback onAimFinishedCallback)
    {
        while(states.IsAiming)
            yield return null;

        AimEnd(onAimFinishedCallback);

    }
    
    //AimEnd + Callback
    private void AimEnd(OnAimFinishedCallback onAimFinishedCallback)
    {
        onAimFinishedCallback();
    }

}
