using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AimButtonBehaviour : MonoBehaviour {

    public Transform aimDirectionAsset;
    private Vector2 inputPosition;
    public Transform car;
    private Transform aimButton;
    public Vector2 defaultPosition;
    Vector2 touchPoint;

    // Use this for initialization

    void Start () {
        aimButton = GetComponent<Transform>();
	}
	


	// Update is called once per frame
	public void moveAimButtonToTouchPosition () {


        touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimButton.position = touchPoint;
        
	}

    public void resetPosition()
    {

        aimButton.localPosition = defaultPosition;
    }
}
