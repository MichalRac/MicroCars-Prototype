using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;
    //public float maxDistanceX;
    //public float maxDistanceY;

    private AimingPositioning aimButtonPositioning;
    private Transform cameraPoint;
    private Vector2 focusPoint;
    private Rigidbody2D targetRB;
    private Quaternion nextRotation;


    public float cameraRotationSpeed;
    public float cameraSpeed = 1.0f;

	void Start ()
    {
        cameraPoint = GetComponent<Transform>();
        targetRB = target.GetComponent<Rigidbody2D>();     // It will be called in update so it should be better to use a reference rather that getting component each frame
        aimButtonPositioning = target.GetComponent<AimingPositioning>();
    }
	
	void Update ()
    {
        focusPoint = target.transform.position;

        /* // uncomment and add to optimalX and optimalY below for camera to also take aim button into consideration
        Vector3 aimButtonPosition = aimButtonPositioning.GetAimButtonPosition();
        Vector3 aimButtonDeltaPosition = new Vector3(
            Mathf.Abs(focusPoint.x - aimButtonPosition.x),
            Mathf.Abs(focusPoint.y - aimButtonPosition.y),
            -10.0f);
        */

        float optimalX = focusPoint.x + targetRB.velocity.x;
        float optimalY = focusPoint.y + targetRB.velocity.y;

        Vector3 cameraOptimalPoint = new Vector3(optimalX, optimalY, -10.0f);
        cameraPoint.position = Vector3.MoveTowards(cameraPoint.position, cameraOptimalPoint, cameraSpeed);
	}

    /*
     * Depricated functionality for camera turning with player's rotation (always matching player's rotation)
    public void fixCamera()
    {
        StartCoroutine(slowlyFixRotation());
    }

    IEnumerator slowlyFixRotation()
    {
        nextRotation = target.transform.rotation;
        while (Quaternion.Angle(cameraPoint.rotation, target.transform.rotation) > 0.001f)
        {
            cameraPoint.rotation = Quaternion.RotateTowards(cameraPoint.rotation, nextRotation, cameraRotationSpeed);
            yield return null;
        }
        
    }
    */
}
