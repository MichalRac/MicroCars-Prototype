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

    public void AimTurnStart()
    {
        states.IsAiming = true;
        positioning.ShowAimButton();

        StartCoroutine(aimingLifetime());

    }

    //AimDuration Right now does nothing, but there are many possibilities to add here (particles, sound etc.)
    private IEnumerator aimingLifetime()
    {
        while (states.IsAiming)
            yield return null;

        AimEnd();

    }

    // For UI EventTrigger
    public void OnAimHold()
    {
        positioning.MoveAimToPointer();
    }

    // For UI EventTrigger
    public void OnAimRelease()
    {
        positioning.onAimRelease();
        states.IsAiming = false;
    }

    //AimEnd Right now does nothing, but there are many possibilities to add here (particles, sound etc.)
    private void AimEnd()
    {
    }

}
