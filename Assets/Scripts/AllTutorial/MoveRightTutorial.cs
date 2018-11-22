using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoveRightTutorial : Tutorial
{
    public GameObject paddleEnd;
    public GameObject waterLevel;
    public GameObject Camera;

    public override void CheckIfHappening()
    {
        if (paddleEnd.transform.position.y < waterLevel.transform.position.y && paddleEnd.transform.position.x > Camera.transform.position.x)
		{
			
			TutorialManager.Instace.CompletedTutorial();
			print("right");
		}
            
    }
}