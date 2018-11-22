using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPaddleTutorial: Tutorial {

    public GameObject paddleEnd;
    public GameObject waterLevel;

    public override void CheckIfHappening()
    {
        if (paddleEnd.transform.position.y > (waterLevel.transform.position.y + 15.0))
        {
            TutorialManager.Instace.CompletedTutorial();
            Debug.Log(paddleEnd.transform.position.y + " " + waterLevel.transform.position.y);
            Debug.Log("finish tutorial1");
        }
    }
}
