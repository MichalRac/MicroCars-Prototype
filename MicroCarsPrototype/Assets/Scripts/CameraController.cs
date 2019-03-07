using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;
    protected Transform cameraPoint;
    protected Vector2 focusPoint;
    private Quaternion nextRotation;

	// Use this for initialization
	void Start () {
        cameraPoint = GetComponent<Transform>();
        StartCoroutine(slowlyFixRotation());
        
    }
	
	// Update is called once per frame
	void Update () {
        /*
        focusPoint = target.transform.position;
        cameraPoint.position = new Vector3(focusPoint.x, focusPoint.y, -10.0f);
        cameraPoint.rotation = target.transform.rotation;
        */
        
	}

    public void fixCamera()
    {
        StartCoroutine(slowlyFixRotation());
    }

    IEnumerator slowlyFixRotation()
    {
        nextRotation = target.transform.rotation;
        while (Quaternion.Angle(cameraPoint.rotation, target.transform.rotation) > 1.0f)
        {
            cameraPoint.rotation = Quaternion.RotateTowards(cameraPoint.rotation, nextRotation, 0.5f);
            yield return null;
        }
        
    }
}
