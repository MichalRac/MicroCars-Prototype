using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsMovement : MonoBehaviour
{

    [SerializeField] private float _carSpeed = 0.0f;
    [SerializeField] private float _carMaxVelocityMagnitude = 7.5f;
    private Rigidbody2D _rb2D;
    private CarPhysicsDynamicDrag _carDynamicDrag;

    private float _minMovingSpeed; // Value passed from CarPhysicsRoot TODO: Think of a better solution to passing this one value
    public float MinMovingSpeed { get => _minMovingSpeed; set => _minMovingSpeed = value; }


    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _carDynamicDrag = GetComponent<CarPhysicsDynamicDrag>();
    }

    public void Move(float moveForce)
    {
        Vector2 moveForceVector = new Vector2(0.0f, moveForce * _carSpeed);
        _rb2D.AddRelativeForce(moveForceVector);

        StartCoroutine(_carDynamicDrag.StartDynamicDrag());

        StartCoroutine(MaintainVelocityRotation());
    }

    public IEnumerator MaintainVelocityRotation()
    {
        while (_rb2D.velocity.magnitude > MinMovingSpeed)
        {

            Vector3 velocity = _rb2D.velocity;
            _rb2D.velocity = _rb2D.transform.up * velocity.magnitude;
            //rb2D.velocity = transform.forward * velocity.magnitude;
            yield return new WaitForFixedUpdate();
        }

    }

    private void Update()
    {
        if(_rb2D.velocity.magnitude > _carMaxVelocityMagnitude)
        {
            _rb2D.velocity = _rb2D.velocity.normalized * _carMaxVelocityMagnitude;
        }
    }
}
