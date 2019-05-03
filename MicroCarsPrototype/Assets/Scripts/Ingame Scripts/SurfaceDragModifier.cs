using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDragModifier : MonoBehaviour
{
    public float modifier;
    public CarDynamicDrag carDynamicDrag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("EnteredTrigger");
        carDynamicDrag.surfaceDragModifiers *= modifier;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("ExitedTrigger");
        carDynamicDrag.surfaceDragModifiers /= modifier;
    }

}
