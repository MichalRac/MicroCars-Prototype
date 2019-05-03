using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UISceneControler : MonoBehaviour {

    public Text triesTextObject;
    public Text turnInfo;
    private int tries = 0;



    public void addOneTry()
    {
        tries++;
        triesTextObject.text = "<size=15>Tries:</size>" + tries; 
    }


    public void switchTurnInfo(bool target)
    {
        if (target == true)
            turnInfo.text = "<size=15>Is this your turn:</size> Yup.";

        else if (target == false)
            turnInfo.text = "<size=15> Is this your turn:</size> Nope.";
    }

}
