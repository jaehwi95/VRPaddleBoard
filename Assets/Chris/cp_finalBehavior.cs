using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cp_finalBehavior : MonoBehaviour {

    public AudioClip coinClip;
    public Text finish;
    public GameObject finish_game;
    public Time t;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        AudioSource.PlayClipAtPoint(coinClip, transform.position, 0.5f);
        gameObject.SetActive(false);
        finish_game.SetActive(true);
        finish.text = "Congratulations! You have recorded " + t + "!";
        Time.timeScale = 0;
    }
}