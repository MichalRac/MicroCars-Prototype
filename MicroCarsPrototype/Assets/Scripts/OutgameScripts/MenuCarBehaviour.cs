using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCarBehaviour : MonoBehaviour
{

    Rigidbody2D rb2D;



    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddRelativeForce(new Vector2(0, 250));
    }


    void Update()
    {
        if(gameObject.transform.position.sqrMagnitude > 200.0f)
        {
            Destroy(gameObject);
        }
    }

    
}
