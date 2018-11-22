using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonaCollision : MonoBehaviour {

    public Material mat1;
    public Material mat2;
    public bool isCollide;

    void Start()
    {
        Debug.Log("start");
        Color color1 = mat1.color;
        Color color2 = mat2.color;

        color1.a = 1.0f;
        color2.a = 1.0f;

        mat1.color = color1;
        mat2.color = color2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "board") {
            return;
        }
        isCollide = true;
        Color color1 = mat1.color;
        Color color2 = mat2.color;

        color1.a = 0.3f;
        color2.a = 0.3f;

        mat1.color = color1;
        mat2.color = color2;
        //Debug.Log("trigger enter");
        //colliderPos = gameObject.transform.position;
        //paddleBoardPos = paddleBoard.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "board")
        {
            return;
        }

        isCollide = false;
        Color color1 = mat1.color;
        Color color2 = mat2.color;

        color1.a = 1.0f;
        color2.a = 1.0f;

        mat1.color = color1;
        mat2.color = color2;

    }

}
