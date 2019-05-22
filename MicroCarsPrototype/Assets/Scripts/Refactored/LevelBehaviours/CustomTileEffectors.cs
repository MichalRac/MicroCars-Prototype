using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTileEffectors : MonoBehaviour
{
    
    public GameObject player;
    private CarPhysicsRoot carPhysicsRoot;
    private CarPhysicsTurning carTurning;
    private GameObject effector;

    public float accelaratePower;
    public float turnTime;
    public float effectorAngle;



    // Start is called before the first frame update
    void Start()
    {
        carTurning = player.GetComponent<CarPhysicsTurning>();
        carPhysicsRoot = player.GetComponent<CarPhysicsRoot>();
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("TileEffector TriggerEnter");
        CarPhysicsRoot carPhysicsScript = collision.GetComponent<CarPhysicsRoot>();
        if (carPhysicsScript == null)
        {
            Debug.Log("CarPhysics not found");
            return;
        }
        else
        {
            carTurning.TurnOnCustomTurnEffect(turnTime, effectorAngle);
            carPhysicsRoot.InitializeMovement(accelaratePower);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        carTurning.TurnOffCustomTurnEffect();
    }

}
