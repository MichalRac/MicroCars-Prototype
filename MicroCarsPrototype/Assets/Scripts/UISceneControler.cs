using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UISceneControler : MonoBehaviour {

    public Text triesTextObject;
    private int tries = 0;

    void addOneTry()
    {
        tries++;
        triesTextObject.text = "<size=15>Tries:</size>" + tries; 
    }

}
