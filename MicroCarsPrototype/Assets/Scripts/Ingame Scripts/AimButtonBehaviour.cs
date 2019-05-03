using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AimButtonBehaviour : MonoBehaviour {

    //this gameobject's transform reference
    private Transform aimButton;

    [Header("Referenced GameObjects")]
    public Transform player;
    public Transform car;
    public Transform ghostCar;
    public Transform aimDirectionAsset;
    public Text aimPowerValueText;

    [Header("Referenced Scripts")]
    public CarPhysics carPhysics;
    public CameraController cameraController;
    public GameController gameController;

    [Header("Default AimButton Position")]
    public Vector2 defaultButtonPosition;

    /// <summary>
    /// This script is responsible for Aiming System, together with Player GameObject which is being aimed, and GameController, which is responsible
    /// for deciding when we can actually aim.
    /// </summary>



    private void Awake()
    {
        aimButton = GetComponent<Transform>();
    }


    public void resetPosition()
    {
        //Reseting the aim assets after input ends
        aimButton.localPosition = defaultButtonPosition;
        aimDirectionAsset.localPosition = -defaultButtonPosition;
        ghostCar.localPosition = Vector2.zero;
    }


    public void resetRotation()
    {
        Quaternion tagetRotation = ghostCar.rotation;
        Quaternion reset = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        ghostCar.localRotation = reset;
        car.localRotation = reset;
        player.rotation = tagetRotation;

        cameraController.fixCamera();
    }


    public float calculatePower()
    {
        float aimPower = Vector2.Distance(aimButton.position, player.transform.position);

        if (aimPower < 1.0f)
            return 0;

        return aimPower;
    }


    public void displayAimAssets(bool setDisplay)
    {

        aimDirectionAsset.gameObject.SetActive(setDisplay);
        ghostCar.gameObject.SetActive(setDisplay);
    }


    public void showAimButton()
    {
        aimButton.gameObject.SetActive(true);
    }


    // Two methods below are used by event system when AimButton is held down (event system)
    public void moveAimButtonToTouchPosition ()
    {
        //Finding input position and moving the aim button there
        Vector2 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimButton.position = touchPoint;
        moveDirectionArrow();
	}


    // TODO: Make it so that holding button on either side of the screen slowly rotates the view
    public void moveDirectionArrow()
    {
        //Displaying the aim arrow IF the aim was pulled far enough
        if (calculatePower() > 0)
            displayAimAssets(true);
        else
            displayAimAssets(false);
        

        //Setting the aim direction arrow to the opposite of aim button
        aimDirectionAsset.localPosition = -aimButton.localPosition;

        // Calculating rotation towards which we aim
        Vector2 direction = aimButton.localPosition - car.localPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Setting up the rotation for the aim button, car, and ghost car
        aimDirectionAsset.localRotation = rotation;
        car.localRotation = rotation;
        ghostCar.localRotation = rotation;
        //TODO: find if there is a more efficient way to set up ghost car position
        ghostCar.localPosition = aimButton.localPosition;
        ghostCar.position = Vector2.MoveTowards(ghostCar.position, player.position, 0.5f);

        aimPowerValueText.text = calculatePower().ToString();
    }


    //Two methods below are used by event system when AimButton has stopped being pressed (event system)
    public void onAimRelease()
    {
        if (calculatePower() == 0)
        {
            resetPosition();
            resetRotation();
            return;
        }



        displayAimAssets(false);
        resetRotation();
        gameController.addOneTryCount();
        gameController.switchTurnState(false);
    
        //Since the resetRotation() didn't apply fast enough, I had to delay the actual movement until next frame
        //TODO: Try to make this work without the need of coroutine
        StartCoroutine("moveNextFrame");
    }



    public IEnumerator moveNextFrame()
    {
        yield return null;
        carPhysics.Move(calculatePower());
        resetPosition();
        aimButton.gameObject.SetActive(false);

        // Before finishing this coroutine we start another which will be waiting for the movement to stop.
        carPhysics.StartCoroutine("startNextTurnWhenStopped");
    }


}

