using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTileEffectors : MonoBehaviour
{
    /*
    public GameObject player;
    private CarPhysics carPhysics;
    private GameObject effector;

    public float accelaratePower;
    public float turnTime;
    public float effectorAngle;



    // Start is called before the first frame update
    void Start()
    {
        carPhysics = player.GetComponent<CarPhysics>();
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("TileEffector TriggerEnter");
        CarPhysics carPhysicsScript = collision.GetComponent<CarPhysics>();
        if (carPhysicsScript == null)
        {
            Debug.Log("CarPhysics not found");
            return;
        }
        else
        {
            carPhysicsScript.triggerCustomTurnEffect(turnTime, effectorAngle);
            carPhysicsScript.Move(accelaratePower);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        carPhysics.StopCoroutine("customTurnEffect");
    }

    */
}
