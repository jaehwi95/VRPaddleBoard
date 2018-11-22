using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackTtr : Tutorial {

    public GameObject paddleEnd;
    public GameObject waterLevel;
    public GameObject Camera;

    public override void CheckIfHappening()
    {
        if (paddleEnd.transform.position.y < waterLevel.transform.position.y && paddleEnd.transform.position.z < Camera.transform.position.z)
		{
			
			TutorialManager.Instace.CompletedTutorial();
			print("back");
		}
    }
}
