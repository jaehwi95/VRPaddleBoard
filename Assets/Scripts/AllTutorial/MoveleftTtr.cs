using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveleftTtr : Tutorial
{

    public GameObject paddleEnd;
    public GameObject waterLevel;
    public GameObject Camera;

    public override void CheckIfHappening()
    {
        if (paddleEnd.transform.position.y < waterLevel.transform.position.y && paddleEnd.transform.position.x < Camera.transform.position.x)
		{
			
			TutorialManager.Instace.CompletedTutorial();
			print("left");
		}
            
    }
}
