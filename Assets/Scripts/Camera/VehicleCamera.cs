﻿using UnityEngine;
using System.Collections;

public class VehicleCamera : MonoBehaviour {
	public Camera cam;
	public Transform cameraLookat;
	public float sensitivity;
	public float unscopeFOV;
	public float scopeFOV;
	public float FOVlerp;

	private Vector3 initialPos;
	private Vector3 relativePos;  // camera relative position to vehicle (transform.position)
	private float targetFOV;

	void Start () {
		initialPos = cam.transform.position - transform.position;
		relativePos = initialPos;
		targetFOV = unscopeFOV;
		GetComponent<ControlRegistration> ().RegisterControl += RegisterControl;
		GetComponent<ControlRegistration> ().UnregisterControl += UnregisterControl;
	}

	void RegisterControl(){
		cam.transform.position = transform.position + initialPos;
		cam.transform.parent = null;
		cam.transform.up = Vector3.up;
		KeyboardEventHandler.MouseMovement += MouseMoved;
		KeyboardEventHandler.Fire2_Keydown += Scope;
		KeyboardEventHandler.Fire2_Keyup += Unscope;
	}

	void UnregisterControl(){
		cam.transform.parent = transform;
		KeyboardEventHandler.MouseMovement -= MouseMoved;
		KeyboardEventHandler.Fire2_Keydown -= Scope;
		KeyboardEventHandler.Fire2_Keyup -= Unscope;
	}
	
	// Update is called once per frame
	void Update () {
		// update position of camera
		cam.transform.position = transform.position + relativePos;
		cam.transform.forward = (cameraLookat.position - cam.transform.position);
		// update FOV of camera
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, FOVlerp * Time.deltaTime);
	}

	void MouseMoved(float x, float y){
		relativePos = Quaternion.AngleAxis(x*sensitivity, Vector3.up) * relativePos;
		relativePos = Quaternion.AngleAxis (-y * sensitivity, cam.transform.right) * relativePos;
	}

	void Scope(){
		targetFOV = scopeFOV;
	}

	void Unscope(){
		targetFOV = unscopeFOV;
	}
}
