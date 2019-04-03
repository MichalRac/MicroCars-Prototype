using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTileEffectors : MonoBehaviour
{
    public GameObject player;
    private CarPhysics carPhysics;
    private GameObject effector;

    public float effectorAngle;

    // Start is called before the first frame update
    void Start()
    {
        carPhysics = player.GetComponent<CarPhysics>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CarPhysics carPhysicsScript = other.GetComponent<CarPhysics>();
        if(carPhysicsScript != null)
        {
            
        }
    }



}
