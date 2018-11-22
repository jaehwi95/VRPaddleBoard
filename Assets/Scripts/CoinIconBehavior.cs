using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinIconBehavior : MonoBehaviour {
	public GameObject global;
    public GameObject board;
	
	// Use this for initialization
	void Start () {
		//Physics.IgnoreCollision(board.GetComponent<Collider>(), GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider collider) {
        print("collider: " + collider.tag);
        if (collider.tag == "paddle") {
            gameObject.SetActive(false);
        }
    }
}