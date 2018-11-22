using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpBehavior : MonoBehaviour {

    public AudioClip coinClip;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        print("collider: " + collider.tag);
        AudioSource.PlayClipAtPoint(coinClip, transform.position, 0.5f);
        gameObject.SetActive(false);
    }
}