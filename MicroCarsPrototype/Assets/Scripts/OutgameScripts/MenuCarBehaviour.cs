using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCarBehaviour : MonoBehaviour
{

    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddRelativeForce(new Vector2(0, 250));
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.sqrMagnitude > 200.0f)
        {
            Destroy(gameObject);
        }
        
    }
}
