using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimingPositioning))]
public class AimingRoot : MonoBehaviour
{
    
    [HideInInspector]
    private AimingPositioning positioning;
    private bool stateIsAiming = false;

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
        positioning.onAimRelease();
        stateIsAiming = false;
    }

    // AimStart
    public void AimStart(OnTurnEndCallback onTurnEndCallback)
    {
        stateIsAiming = true;
        positioning.ShowAimButton();

        StartCoroutine(aimingLifetime(onTurnEndCallback));
        
    }

    //AimDuration
    private IEnumerator aimingLifetime(OnTurnEndCallback onTurnEndCallback)
    {
        while(stateIsAiming)
            yield return null;

        AimEnd(onTurnEndCallback);

    }
    
    //AimEnd + Callback
    private void AimEnd(OnTurnEndCallback onTurnEndCallback)
    {
        onTurnEndCallback();
    }

}
