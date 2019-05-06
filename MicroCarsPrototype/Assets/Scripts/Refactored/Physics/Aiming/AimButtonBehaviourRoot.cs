using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimButtonBehaviourOperations))]
public class AimButtonBehaviourRoot : CarPhysicsRoot
{
    
    protected AimButtonBehaviourOperations aimButtonBehaviourOperations;
    

    
    private void Start()
    {
        
        aimButtonBehaviourOperations = GetComponent<AimButtonBehaviourOperations>();
        
    }


    // Call this each frame the button is held down
    public void onAimHold()
    {
        aimButtonBehaviourOperations.moveAimToPointer();
    }


    //Two methods below are used by event system when AimButton has stopped being pressed (event system)
    public void onAimRelease()
    {
        if (aimButtonBehaviourOperations.calculatePower() == 0)
        {
            aimButtonBehaviourOperations.resetPosition();
            aimButtonBehaviourOperations.resetRotation();
            return;
        }



        aimButtonBehaviourOperations.displayAimAssets(false);
        aimButtonBehaviourOperations.resetRotation();
        gameController.addOneTryCount();
        gameController.switchTurnState(false);

        //Since the resetRotation() didn't apply fast enough, I had to delay the actual movement until next frame
        //TODO: Try to make this work without the need of coroutine
        StartCoroutine(moveNextFrame());
    }


    public IEnumerator moveNextFrame()
    {
        yield return null;
        base.initializeMovement(aimButtonBehaviourOperations.calculatePower());
        aimButtonBehaviourOperations.resetPosition();
        aimButtonBehaviourOperations.aimButton.gameObject.SetActive(false);

        // Before finishing this coroutine we start another which will be waiting for the movement to stop.
        base.StartCoroutine(startNextTurnWhenStopped());
    }
}
