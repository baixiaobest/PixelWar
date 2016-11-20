﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float walkSpeed;
	public float runSpeed;
	public float jumpSpeed;
	public float mouseSensitivity;
	public Camera cam;
	public float speedLerp;
	public LayerMask groundMask;
	public Transform cameraPivot;

	public enum MovementMode {Ground, Air};

	private Vector3 speedVec;
	private MovementMode movementMode=MovementMode.Ground;
	private bool running =false;
	private Rigidbody rigid;

	void Start () {
		rigid = GetComponent<Rigidbody> ();
		speedVec = new Vector3 (0, 0, 0);
		KeyboardEventHandler.W_Forward += Forward;
		KeyboardEventHandler.S_Backward += Backward;
		KeyboardEventHandler.A_Left += Left;
		KeyboardEventHandler.D_Right += Right;
		KeyboardEventHandler.Is_Running += Run;
		KeyboardEventHandler.Is_Walking += Walk;
		KeyboardEventHandler.MouseMovement += RotateView;
		KeyboardEventHandler.Space_Keydown += Jump;
	}

	void Update () {
		switch (movementMode) {
		case MovementMode.Ground:
			MoveOnGround ();
			break;
		case MovementMode.Air:
			MoveInAir ();
			break;
		}
	}

	private void MoveInAir(){
		if (!running) {
			rigid.velocity = Vector3.Lerp (new Vector3(rigid.velocity.x, 0, rigid.velocity.z), walkSpeed * speedVec, speedLerp) + new Vector3(0, rigid.velocity.y, 0);
		} else {
			rigid.velocity = Vector3.Lerp (new Vector3(rigid.velocity.x, 0, rigid.velocity.z), runSpeed * speedVec, speedLerp) + new Vector3(0, rigid.velocity.y, 0);
		}
		ClearSpeed ();
	}

	private void MoveOnGround(){
		if (!running) {
			rigid.velocity = Vector3.Lerp (rigid.velocity, walkSpeed * speedVec, speedLerp);
		} else {
			rigid.velocity = Vector3.Lerp (rigid.velocity, runSpeed * speedVec, speedLerp);
		}
		ClearSpeed ();
	}

	private void Forward(){
		speedVec += transform.forward;
		speedVec = speedVec.normalized;
	}
	private void Backward(){
		speedVec -= transform.forward;
		speedVec = speedVec.normalized;
	}
	private void Left(){
		speedVec -= transform.right;
		speedVec = speedVec.normalized;
	}
	private void Right(){
		speedVec += transform.right;
		speedVec = speedVec.normalized;
	}
	private void Run(){
		if(movementMode != MovementMode.Air)
			running = true;
	}
	private void Walk(){
		running = false;
	}
	private void ClearSpeed(){
		speedVec = new Vector3 (0,0,0);
	}

	private void RotateView(float x, float y){
		//Debug.Log ("rotate");
		transform.RotateAround (transform.position, Vector3.up, mouseSensitivity*x);

		float angleToUp = Vector3.Angle (cam.transform.forward, Vector3.up);
		if (angleToUp < 10)
			y = y > 0 ? 0 : y;
		if (angleToUp > 170)
			y = y < 0 ? 0 : y;

		cam.transform.RotateAround (cameraPivot.position, -transform.right, mouseSensitivity*y);
	}

	private void Jump(){
		if (movementMode == MovementMode.Ground)
			rigid.velocity = new Vector3 (rigid.velocity.x, jumpSpeed, rigid.velocity.z);//Vector3.up * jumpSpeed;
	}

	void OnCollisionStay(Collision col){
		if ((1 << col.gameObject.layer & groundMask) != 0) {
			movementMode = MovementMode.Ground;
		}
	}

	void OnCollisionExit(Collision col){
		if ((1 << col.gameObject.layer & groundMask) != 0) {
			movementMode = MovementMode.Air;
		}
	}

	public void SetMovementMode(MovementMode mode){
		movementMode = mode;
	}
}
