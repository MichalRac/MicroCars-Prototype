using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTileEffectors : MonoBehaviour
{
    public GameObject player;
    private CarPhysicsRoot _carPhysicsRoot;
    private CarPhysicsTurning _carTurning;
    private GameObject _effector;

    public float accelaratePower;
    public float turnTime;
    public float effectorAngle;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject;
        _carTurning = player.GetComponent<CarPhysicsTurning>();
        _carPhysicsRoot = player.GetComponent<CarPhysicsRoot>();
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
            _carTurning.TurnOnCustomTurnEffect(turnTime, effectorAngle);
            _carPhysicsRoot.InitializeMovement(accelaratePower);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _carTurning.TurnOffCustomTurnEffect();
    }

}
