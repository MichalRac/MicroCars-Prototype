using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimingPositioning : MonoBehaviour
{
    private AimingRoot aimingRoot;

    private GameObject player;
    [SerializeField]
    private GameObject car;
    [SerializeField]
    private GameObject ghostCar;
    [SerializeField]
    private GameObject aimArrow;
    [SerializeField]
    private GameObject aimButton;

    private GameObject aimCanvas;
    public Vector2 defaultButtonPosition = new Vector2(0.0f, 0.0f);



    private void Start()
    {
            // Getting needed references
        player = GetComponent<GameObject>();
        aimCanvas = gameObject.transform.Find("AimCanvas").gameObject;
        aimButton = aimCanvas.transform.Find("AimButtonArea").gameObject;

            // Instantiating necessary assets TODO move this to a different class
        car = Instantiate(car, transform) as GameObject;
        ghostCar = Instantiate(ghostCar, transform) as GameObject;
        aimArrow = Instantiate(aimArrow, transform) as GameObject;

            // Setting unnecesary assets initial state as inactive
        ghostCar.SetActive(false);
        aimArrow.SetActive(false);
    }

    // Calculating the power of the aim at the moment
    public float CalculatePower()
    {
        float aimPower = Vector2.Distance(aimButton.transform.position, gameObject.transform.position);

        if (aimPower < 1.0f)
            return 0;

        return aimPower;
    }

    public void ResetPosition()
    {
        //Reseting the aim assets after input ends
        aimButton.transform.localPosition = defaultButtonPosition;
        aimArrow.transform.localPosition = -defaultButtonPosition;
        ghostCar.transform.localPosition = Vector2.zero;
    }

    public void ResetRotation()
    {
        Quaternion tagetRotation = ghostCar.transform.rotation;
        Quaternion reset = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        ghostCar.transform.localRotation = reset;
        car.transform.localRotation = reset;
        gameObject.transform.rotation = tagetRotation;

        //cameraController.fixCamera();     //Not working after refactoring, as camera rotation fixing is no longer needed.
    }

    public void MoveAimToPointer()
    {
        Vector2 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimButton.transform.position = touchPoint;
        MoveDirectionArrow();
    }

    public float ReleaseAndReset()
    {
        if (CalculatePower() == 0)
        {
            ResetPosition();
            ResetRotation();
            return -1;
        }

        float power = CalculatePower();
        DisplayAimAssets(false);
        ResetRotation();

        return power;
    }

    public void onAimRelease()
    {
        if (CalculatePower() == 0)
        {
            ResetPosition();
            ResetRotation();
            return;
        }

        DisplayAimAssets(false);
        ResetRotation();
            //gameController.addOneTryCount();
            //gameController.switchTurnState(false);

            //Since the resetRotation() didn't apply fast enough, I had to delay the actual movement until next frame
            //TODO: Try to make this work without the need of coroutine
        StartCoroutine(moveNextFrame());
    }

    public void DisplayAimAssets(bool setDisplay)
    {

        aimArrow.transform.gameObject.SetActive(setDisplay);
        ghostCar.gameObject.SetActive(setDisplay);
    }

    public void ShowAimButton()
    {
        aimButton.gameObject.SetActive(true);
    }

    public void MoveDirectionArrow()
    {
            //Displaying the aim arrow IF the aim was pulled far enough
        if (CalculatePower() > 0)
            DisplayAimAssets(true);
        else
            DisplayAimAssets(false);


            //Setting the aim direction arrow to the opposite of aim button
        aimArrow.transform.localPosition = -aimButton.transform.localPosition;

            // Calculating rotation towards which we aim
        Vector2 direction = aimButton.transform.localPosition - car.transform.localPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //Setting up the rotation for the aim button, car, and ghost car
        aimArrow.transform.localRotation = rotation;
        car.transform.localRotation = rotation;
        ghostCar.transform.localRotation = rotation;
            //TODO: find if there is a more efficient way to set up ghost car position
        ghostCar.transform.localPosition = aimButton.transform.localPosition;
        ghostCar.transform.position = Vector2.MoveTowards(ghostCar.transform.position, gameObject.transform.position, 0.5f);

            //aimPowerValueText.text = calculatePower().ToString();         // For displaying the power of currenty aimed button.
    }
    
    public IEnumerator moveNextFrame()
    {
        yield return null;
        gameObject.GetComponent<CarPhysicsRoot>().InitializeMovement(CalculatePower());
        ResetPosition();
        aimButton.gameObject.SetActive(false);

            // Before finishing this coroutine we start another which will be waiting for the movement to stop.
        gameObject.GetComponent<CarPhysicsRoot>().StartCoroutine("StartNextTurnWhenStopped");
    }
    
}
