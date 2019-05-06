using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimButtonBehaviourOperations : AimButtonBehaviourRoot
{

    protected GameObject player;
    [SerializeField]
    protected GameObject car;
    [SerializeField]
    protected GameObject ghostCar;
    [SerializeField]
    protected GameObject aimArrow;
    [SerializeField]
    private GameObject aimCanvas;
    public GameObject aimButton;

    public Vector2 defaultButtonPosition;       //Legacy, used back when button didn't take up entire screen.



    private void Start()
    {
        player = GetComponent<GameObject>();

        Instantiate(aimCanvas, transform);
        aimButton = gameObject.transform.Find("AimCanvas(Clone)/AimButtonArea").gameObject;

        
        car = Instantiate(car, transform);
        //car.SetActive(true);
        ghostCar = Instantiate(ghostCar, transform);
        ghostCar.SetActive(false);
        aimArrow = Instantiate(aimArrow,transform);
        aimArrow.SetActive(false);
        


        //base.aimButton = this.aimButton;
    }


    public void resetPosition()
    {
        //Reseting the aim assets after input ends
        aimButton.transform.localPosition = defaultButtonPosition;
        aimArrow.transform.localPosition = -defaultButtonPosition;
        ghostCar.transform.localPosition = Vector2.zero;
    }

    public void moveAimToPointer()
    {
        Vector2 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimButton.transform.position = touchPoint;
        moveDirectionArrow();
    }

    public void resetRotation()
    {
        Quaternion tagetRotation = ghostCar.transform.rotation;
        Quaternion reset = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        ghostCar.transform.localRotation = reset;
        car.transform.localRotation = reset;
        player.transform.rotation = tagetRotation;

        //cameraController.fixCamera();     //Not working after refactor, as camera rotation fixing is no longer needed.
    }


    public float calculatePower()
    {
        float aimPower = Vector2.Distance(aimButton.transform.position, player.transform.position);

        if (aimPower < 1.0f)
            return 0;

        return aimPower;
    }


    public void displayAimAssets(bool setDisplay)
    {

        aimArrow.transform.gameObject.SetActive(setDisplay);
        ghostCar.gameObject.SetActive(setDisplay);
    }


    public void showAimButton()
    {
        aimButton.gameObject.SetActive(true);
    }


    public void moveDirectionArrow()
    {
        //Displaying the aim arrow IF the aim was pulled far enough
        if (calculatePower() > 0)
            displayAimAssets(true);
        else
            displayAimAssets(false);


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
        ghostCar.transform.position = Vector2.MoveTowards(ghostCar.transform.position, player.transform.position, 0.5f);

        //aimPowerValueText.text = calculatePower().ToString();         // For displaying the power of currenty aimed button.
    }
}
