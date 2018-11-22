using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text counterText;

    public float seconds, minutes, milliseconds;
	// Use this for initialization
	void Start () {
        counterText = GetComponent<Text>() as Text;
	}
	
	// Update is called once per frame
	void Update () {
        minutes = (int)(Time.timeSinceLevelLoad / 60f) % 60;
        seconds = (int)(Time.timeSinceLevelLoad % 60f);
        milliseconds = (int)(Time.timeSinceLevelLoad * 1000f) % 1000 / 10;
        counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
	}
}
