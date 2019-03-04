using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AimButtonBehaviour : MonoBehaviour {


    private Vector2 touchPoint;
    private Vector2 localPos;
    private Transform aimButton;

    public Transform player;
    public Transform car;
    public Transform ghostAimer;
    public Transform aimDirectionAsset;
    public Vector2 defaultPosition;


    // Use this for initialization

    void Start ()
    {
        aimButton = GetComponent<Transform>();
	}
	


	// Update is called once per frame
	public void moveAimButtonToTouchPosition ()
    {
        //Finding input position and moving the aim button there
        touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimButton.position = touchPoint;
        moveDirectionArrow();
	}


    // TODO: Make it so that holding button on either side of the screen slowly rotates the view
    public void moveDirectionArrow()
    {
        //Setting the aim arrow to the opposite of aim button
        aimDirectionAsset.localPosition = -aimButton.localPosition;

        // Calculating rotation we want towards which we aim
        Vector2 direction = aimButton.localPosition - car.localPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Setting up the rotation for the aim button, car, and ghost car
        aimDirectionAsset.localRotation = rotation;
        car.localRotation = rotation;
        ghostAimer.localRotation = rotation;
        //TODO: find if there is a more efficient way to set up ghost car position
        ghostAimer.localPosition = aimButton.localPosition;
        ghostAimer.position = Vector2.MoveTowards(ghostAimer.position, player.position, 0.5f);


    }

    public void resetPosition()
    {

        //Reseting the aim assets after input ends
        aimButton.localPosition = defaultPosition;
        aimDirectionAsset.localPosition = -defaultPosition;
        ghostAimer.localPosition = player.position;
    }
}
