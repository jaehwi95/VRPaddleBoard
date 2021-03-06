﻿//
// PhaseSpace, Inc. 2014
//
using UnityEngine;
using System;
using System.Collections;
using PhaseSpace;

//
// Note:   Use PSRigidCameraController for cameras.
//

public class OWLRigidController : MonoBehaviour
{
	public int RigidID = 0;
	public OWLTracker Tracker;
	public int Interp = 0;
	public Vector3 Scale = new Vector3 (1.0f, 1.0f, 1.0f);
			
	public PhaseSpace.Rigid lastRigid = new PhaseSpace.Rigid();
	
	protected Vector3 prevPos = new Vector3 (0, 0, 0);
	protected Quaternion prevRot = new Quaternion (0, 0, 0, 1);		
	
	// interpolation
	protected int interp_index = 0;
	protected Quaternion[] quaternions = new Quaternion[8];
	protected Vector3[] rotations = new Vector3[8];

	// Use this for initialization
	public virtual void Start ()
	{

	}

	// Update is called once per frame
	public virtual void Update ()
	{
		_Update ();
	}

	protected void _Update ()
	{
		if(Tracker == null) 
		{
			Tracker = Component.FindObjectOfType<OWLTracker> ();
			if(Tracker == null) return;
		}
		
		if (enabled && Tracker.Connected ())
		{
			UpdateFromTracker ();
			transform.position = prevPos;
			transform.rotation = prevRot;
			transform.localScale = Scale;
		}
	}

	//
	protected void UpdateFromTracker ()
	{		
		if (!Tracker.Connected ())
			return;
				
		// get data from tracker		
		lastRigid = Tracker.GetRigid (RigidID);
				
		if (lastRigid == null || lastRigid.cond < 0)
			return;

		prevRot = lastRigid.rotation;
		prevPos = lastRigid.position;
    
		// interpolate if required
		if (Interp > 1) {
			quaternions [interp_index] = prevRot;
			rotations [interp_index] = prevPos;
 
			int n = Math.Min (Interp, quaternions.Length);
			float f = 1.0f / n;
			Quaternion q = quaternions [interp_index];
			Vector3 r = rotations [interp_index];
			for (int i = 1; i < n; i++) {
				int index = ((interp_index - i) + quaternions.Length) % quaternions.Length;
				q = Quaternion.Slerp (q, quaternions [index], f);
				r += rotations [index];
			}
			r *= f;
      						
			prevRot = q;						
			prevPos = r;      
 
			interp_index = (interp_index + 1) % quaternions.Length;
		}				
	}
}
