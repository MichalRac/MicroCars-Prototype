using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class playerInjection : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private EventTrigger eventTrigger;


    private void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
    }


}
