using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTileEffectors : MonoBehaviour
{
    public GameObject player;
    private CarPhysics carPhysics;
    private GameObject effector;

    public float turnTime;
    public float effectorAngle;

    // Start is called before the first frame update
    void Start()
    {
        carPhysics = player.GetComponent<CarPhysics>();
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TileEffector TriggerEnter");
        CarPhysics carPhysicsScript = collision.GetComponent<CarPhysics>();
        if (carPhysicsScript == null)
        {
            Debug.Log("CarPhysics not found");
            return;
        }
        else
            carPhysicsScript.triggerCustomTurnEffect(turnTime, effectorAngle);
    }



}
