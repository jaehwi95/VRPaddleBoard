using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour {

    public GameObject Camera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.transform.position);
        transform.Rotate(Vector3.up - new Vector3(0.0f, 180.0f, 0.0f));
    }
}
