using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;
    protected Transform cameraPoint;
    protected Vector2 focusPoint;
    private Quaternion nextRotation;
    public float cameraRotationSpeed;

	// Use this for initialization
	void Start () {
        cameraPoint = GetComponent<Transform>();
        StartCoroutine(slowlyFixRotation());
        
    }
	
	// Update is called once per frame
	void Update () {

        focusPoint = target.transform.position;
        cameraPoint.position = new Vector3(focusPoint.x, focusPoint.y, -10.0f);

        
	}

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
}
