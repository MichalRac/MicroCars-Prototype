using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDragModifier : MonoBehaviour
{
    public float modifier;
    public CarPhysics playerCarPhysics;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("EnteredTrigger");
        playerCarPhysics.surfaceDragModifiers *= modifier;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("ExitedTrigger");
        playerCarPhysics.surfaceDragModifiers /= modifier;
    }

}
