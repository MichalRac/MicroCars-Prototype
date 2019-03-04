using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;
    protected Transform cameraPoint;
    protected Vector2 focusPoint;

	// Use this for initialization
	void Start () {
        cameraPoint = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        focusPoint = target.transform.position;
        cameraPoint.position = new Vector3(focusPoint.x, focusPoint.y, -10.0f);
        cameraPoint.rotation = target.transform.rotation;

	}
}
