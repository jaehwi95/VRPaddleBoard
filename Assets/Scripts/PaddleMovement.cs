using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour {
    public GameObject paddleEnd;
    public GameObject waterLevel;
    public GameObject paddleboardObj;
    public GameObject leftPos;
    public GameObject rightPos;
    public GameObject standingPos;
    //public GameObject tipPos;
    public float forceScale;
    // Use this for initialization

    private Vector3 prevPos;
    private Vector3 currPos;
    private GameObject forcePos;
    private bool isInWater;
    private bool isLeft;
	void Start () {
        isInWater = false;

	}

    // Update is called once per frame
    void Update()
    {
        //paddleboardObj.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0,-4,0), paddleboardObj.transform.position);

        if (paddleEnd.transform.position.y < waterLevel.transform.position.y) //underwater
        {
            if (!isInWater)
            {
                isInWater = true;
                currPos = paddleEnd.transform.position;
            }
            else
            {
                if (GetComponent<KonaCollision>().isCollide) {
                    return;
                }

                prevPos = currPos;
                currPos = paddleEnd.transform.position;
                SteamVR_Controller.Input(4).TriggerHapticPulse(1000);
                SteamVR_Controller.Input(3).TriggerHapticPulse(1000);
                Vector3 localPos = paddleboardObj.transform.InverseTransformPoint(currPos);
                if (localPos.x < 0) //left
                    forcePos = leftPos;
                else
                    forcePos = rightPos;
                Vector3 forward = paddleboardObj.transform.forward;
                
                Vector3 forceDir = prevPos - currPos;

                float depth = Mathf.Abs(currPos.y - waterLevel.transform.position.y);
                float force = Mathf.Min(depth, 10) / 10 * forceScale;
                //print("Depth: " + depth);
                forceDir.y = 0;
                //paddleboardObj.GetComponent<Rigidbody>().AddForceAtPosition(forceDir, forcePos.transform.position);
                forward.y = 0;



                paddleboardObj.GetComponent<Rigidbody>().AddForceAtPosition(forceDir * force, forcePos.transform.position);
                //paddleboardObj.GetComponent<Rigidbody>().AddForceAtPosition(forceDir * forceScale, paddleboardObj.transform.position);
                //print("force position: " + forcePos.transform.position);
                //print("paddleboard position: " + paddleboardObj.transform.position);

            }

            //print("underwater");
        }
        else
        {
            isInWater = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 forward = paddleboardObj.transform.forward;
            paddleboardObj.GetComponent<Rigidbody>().AddForceAtPosition(forward * 1000, leftPos.transform.position);
        }
    }

}
