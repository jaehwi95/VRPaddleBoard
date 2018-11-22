//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NewCoinBehavior : MonoBehaviour
//{
//	public float rotationSpeed;

//	public float bounceHeight;
//	public float bounceSpeed;

//	public AudioClip coinClip;
//	public GameObject board;

//	private float startingHeight;
//	// Use this for initialization
//	void Start()
//	{
//		startingHeight = transform.position.y;
//		Physics.IgnoreCollision(board.GetComponent<Collider>(), GetComponent<Collider>());
//	}


//	// Update is called once per frame
//	void Update()
//	{
//		transformCoin(bounceHeight, bounceSpeed);
//	}

//	private void transformCoin(float deltaPos, float bounceSpeed)
//	{
//		transform.Rotate(0, rotationSpeed * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
//		Vector3 currPos = transform.position;
//		currPos.y = Mathf.Sin(Mathf.Clamp01((Time.time * bounceSpeed) % 1) * Mathf.PI) * bounceHeight + startingHeight;
//		transform.position = currPos;
//	}

//	void OnTriggerEnter(Collider collider)
//	{
//		print("collider: " + collider.tag);
//		if (collider.tag == "paddle")
//		{
//			AudioSource.PlayClipAtPoint(coinClip, transform.position, 0.5f);
//			gameObject.SetActive(false);
//		}
//	}
//}
