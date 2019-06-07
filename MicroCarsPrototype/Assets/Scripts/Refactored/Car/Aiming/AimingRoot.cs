using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimingPositioning))]
public class AimingRoot : MonoBehaviour
{
    //Setting up the class
    #region
        [HideInInspector]
        private AimingPositioning _positioning;
        private CarStates _states;

        OnAimFinishedCallback onAimFinishedCallbackReference;

        private void Start()
        {
            _positioning = GetComponent<AimingPositioning>();
            _states = GetComponent<CarStates>();
        }
    #endregion

    //Start, Duration and Ending of player's aiming turn
    #region
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

        // For Aiming UI EventTrigger
        public void OnAimHold()
        {
            _positioning.OnAimHoldOperations();
        }

        // For Aiming UI EventTrigger
        public void OnAimRelease()
        {
            _positioning.OnAimReleaseOperations();
            _states.IsAiming = false;
        }

        //AimEnd Right now does nothing, but there are many possibilities to add here (particles, sound etc.)
        //Because the turn does not end until we stop movement, so we don't call CarController yet - it will be called from CarMovement class
        private void AimEnd()
        {
        }
    #endregion
}
