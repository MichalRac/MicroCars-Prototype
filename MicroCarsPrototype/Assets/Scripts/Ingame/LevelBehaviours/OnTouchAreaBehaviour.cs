using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchAreaBehaviour : MonoBehaviour
{
    protected string areaName;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name + " triggered " + gameObject.name);
    }
}
