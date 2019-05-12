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


    public GameObject AimButton
    {
        get
        {
            return aimButton;
        }
    }

    public GameObject Player
    {
        get
        {
            return player;
        }
    }

    private void Start()
    {
        player = GetComponent<GameObject>();
        aimCanvas = gameObject.transform.Find("AimCanvas").gameObject;

        car = Instantiate(car, transform) as GameObject;

        ghostCar = Instantiate(ghostCar, transform) as GameObject;
        ghostCar.SetActive(false);

        aimArrow = Instantiate(aimArrow, transform) as GameObject;
        aimArrow.SetActive(false);
    }

    public void ResetPosition()
    {
        //Reseting the aim assets after input ends
        AimButton.transform.localPosition = defaultButtonPosition;
        aimArrow.transform.localPosition = -defaultButtonPosition;
        ghostCar.transform.localPosition = Vector2.zero;
    }

    public void ResetRotation()
    {
        Quaternion tagetRotation = ghostCar.transform.rotation;
        Quaternion reset = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        ghostCar.transform.localRotation = reset;
        car.transform.localRotation = reset;
        Player.transform.rotation = tagetRotation;

        //cameraController.fixCamera();     //Not working after refactor, as camera rotation fixing is no longer needed.
    }

    public void MoveAimToPointer()
    {
        Vector2 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        AimButton.transform.position = touchPoint;
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

    public void DisplayAimAssets(bool setDisplay)
    {

        aimArrow.transform.gameObject.SetActive(setDisplay);
        ghostCar.gameObject.SetActive(setDisplay);
    }


    public void ShowAimButton()
    {
        AimButton.gameObject.SetActive(true);
    }

    public void MoveDirectionArrow()
    {
        //Displaying the aim arrow IF the aim was pulled far enough
        if (CalculatePower() > 0)
            DisplayAimAssets(true);
        else
            DisplayAimAssets(false);


        //Setting the aim direction arrow to the opposite of aim button
        aimArrow.transform.localPosition = -AimButton.transform.localPosition;

        // Calculating rotation towards which we aim
        Vector2 direction = AimButton.transform.localPosition - car.transform.localPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Setting up the rotation for the aim button, car, and ghost car
        aimArrow.transform.localRotation = rotation;
        car.transform.localRotation = rotation;
        ghostCar.transform.localRotation = rotation;
        //TODO: find if there is a more efficient way to set up ghost car position
        ghostCar.transform.localPosition = AimButton.transform.localPosition;
        ghostCar.transform.position = Vector2.MoveTowards(ghostCar.transform.position, gameObject.transform.position, 0.5f);

        //aimPowerValueText.text = calculatePower().ToString();         // For displaying the power of currenty aimed button.
    }

    public float CalculatePower()
    {
        float aimPower = Vector2.Distance(aimButton.transform.position, gameObject.transform.position);

        if (aimPower < 1.0f)
            return 0;

        return aimPower;
    }

}
