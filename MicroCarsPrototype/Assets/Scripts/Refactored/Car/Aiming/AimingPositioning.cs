using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class is responsible for most operation with moving and calculating for the Aiming System
///  TODO Refactor this class for easier readability and clarity.
/// </summary>
public class AimingPositioning : MonoBehaviour
{
    private AimingRoot aimingRoot;

    private GameObject _player;
    private GameObject _ghostCar;
    private GameObject _aimButton;
    [SerializeField] private GameObject _car;
    [SerializeField] private GameObject _aimArrow;

    private GameObject _aimCanvas;
    public Vector2 defaultButtonPosition = new Vector2(0.0f, 0.0f);




    private void Awake()
    {
        // Getting needed references
        _player = this.gameObject;
        _aimCanvas = gameObject.transform.Find("AimCanvas").gameObject;
        _aimButton = _aimCanvas.transform.Find("AimButtonArea").gameObject;
    }

    //Called on Start() from CarController on playerPrefab setup.
    public void injectPlayerChildObjects(GameObject[] gameObjects)
    {
        _car = gameObjects[0];
        _ghostCar = gameObjects[1];
        
        // Disabling since they are not yet required on start
        _ghostCar.SetActive(false);
        _aimArrow.SetActive(false);
    }

    // Calculating the power of the aim at the moment of method call
    public float CalculatePower()
    {
        float aimPower = Vector2.Distance(_aimButton.transform.position, gameObject.transform.position);

        if (aimPower < 1.0f)
            return 0;

        return aimPower;
    }

    //Reseting the aim assets positions after input ends
    public void ResetPosition()
    {
        _aimButton.transform.localPosition = defaultButtonPosition;
        _aimArrow.transform.localPosition = -defaultButtonPosition;
        _ghostCar.transform.localPosition = Vector2.zero;
    }

    //Reseting the aim assets rotations after input ends
    public void ResetRotation()
    {
        Quaternion tagetRotation = _ghostCar.transform.rotation;
        Quaternion reset = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        _ghostCar.transform.localRotation = reset;
        _car.transform.localRotation = reset;
        gameObject.transform.rotation = tagetRotation;
    }

    public void MoveAimToPointer()
    {
        Vector2 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _aimButton.transform.position = touchPoint;
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

        _aimArrow.transform.gameObject.SetActive(setDisplay);
        _ghostCar.gameObject.SetActive(setDisplay);
    }

    public void ShowAimButton()
    {
        _aimButton.gameObject.SetActive(true);
    }

    public void MoveDirectionArrow()
    {
            //Displaying the aim arrow IF the aim was pulled far enough
        if (CalculatePower() > 0)
            DisplayAimAssets(true);
        else
            DisplayAimAssets(false);


            //Setting the aim direction arrow to the opposite of aim button
        _aimArrow.transform.localPosition = -_aimButton.transform.localPosition;

            // Calculating rotation towards which we aim
        Vector2 direction = _aimButton.transform.localPosition - _car.transform.localPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //Setting up the rotation for the aim button, car, and ghost car
        _aimArrow.transform.localRotation = rotation;
        _car.transform.localRotation = rotation;
        _ghostCar.transform.localRotation = rotation;
            //TODO: find if there is a more efficient way to set up ghost car position
        _ghostCar.transform.localPosition = _aimButton.transform.localPosition;
        _ghostCar.transform.position = Vector2.MoveTowards(_ghostCar.transform.position, gameObject.transform.position, 0.5f);

            //aimPowerValueText.text = calculatePower().ToString();         // For displaying the power of currenty aimed button.
    }
    
    public IEnumerator moveNextFrame()
    {
        yield return null;
        gameObject.GetComponent<CarPhysicsRoot>().InitializeMovement(CalculatePower());
        ResetPosition();
        _aimButton.gameObject.SetActive(false);

            // Before finishing this coroutine we start another which will be waiting for the movement to stop.
        gameObject.GetComponent<CarPhysicsRoot>().StartCoroutine("StartNextTurnWhenStopped");
    }

    public Vector3 GetAimButtonPosition()
    {
        return _aimButton.transform.position;
    }
    
}
