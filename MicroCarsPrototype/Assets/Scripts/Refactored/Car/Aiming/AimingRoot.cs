using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimingPositioning))]
public class AimingRoot : MonoBehaviour
{
    
    [HideInInspector]
    private AimingPositioning _positioning;
    private CarStates _states;

    OnAimFinishedCallback onAimFinishedCallbackReference;

    private void Start()
    {
        _positioning = GetComponent<AimingPositioning>();
        _states = GetComponent<CarStates>();
    }

    public void AimTurnStart()
    {
        _states.IsAiming = true;
        _positioning.ShowAimButton();

        StartCoroutine(aimingLifetime());

    }

    //AimDuration Right now does nothing, but there are many possibilities to add here (particles, sound etc.)
    private IEnumerator aimingLifetime()
    {
        while (_states.IsAiming)
            yield return null;

        AimEnd();

    }

    // For UI EventTrigger
    public void OnAimHold()
    {
        _positioning.MoveAimToPointer();
    }

    // For UI EventTrigger
    public void OnAimRelease()
    {
        _positioning.onAimRelease();
        _states.IsAiming = false;
    }

    //AimEnd Right now does nothing, but there are many possibilities to add here (particles, sound etc.)
    private void AimEnd()
    {
    }

}
