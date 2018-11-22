using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public GameObject paddleboardObj;
	// Use this for initialization
	void Start () {
		//transform.SetAsLastSibling(); //move to the front (on parent)
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = paddleboardObj.transform.position;
        Vector3 forward = transform.forward;
        forward.x = paddleboardObj.transform.forward.x;
        forward.z = paddleboardObj.transform.forward.z;
        //transform.forward = forward;
    }
}
