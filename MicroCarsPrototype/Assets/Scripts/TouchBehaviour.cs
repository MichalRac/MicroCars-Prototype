using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.touchCount > 0)
        {
            Touch mainTouch = Input.GetTouch(0);
            Debug.Log(mainTouch.position);
        }
        
        
	}
}
