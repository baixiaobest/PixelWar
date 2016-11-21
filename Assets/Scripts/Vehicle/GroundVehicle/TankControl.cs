using UnityEngine;
using System.Collections;

public class TankControl : MonoBehaviour {
	private TankPhysics tank;

	// Use this for initialization
	void Start () {
		tank = GetComponent<TankPhysics> ();
		GetComponent<ControlRegistration> ().RegisterControl += RegisterControl;
		GetComponent<ControlRegistration> ().UnregisterControl += UnregisterControl;
	}

	void RegisterControl(){
		KeyboardEventHandler.W_Forward += Forward;
		KeyboardEventHandler.S_Backward += BackWard;
		KeyboardEventHandler.A_Left += TurnLeft;
		KeyboardEventHandler.D_Right += TurnRight;
		KeyboardEventHandler.W_Keyup += ZeroMovement;
		KeyboardEventHandler.S_Keyup += ZeroMovement;
		KeyboardEventHandler.A_Keyup += ZeroTurn;
		KeyboardEventHandler.D_Keyup += ZeroTurn;
	}

	void UnregisterControl(){
		KeyboardEventHandler.W_Forward -= Forward;
		KeyboardEventHandler.S_Backward -= BackWard;
		KeyboardEventHandler.A_Left -= TurnLeft;
		KeyboardEventHandler.D_Right -= TurnRight;
		KeyboardEventHandler.W_Keyup -= ZeroMovement;
		KeyboardEventHandler.S_Keyup -= ZeroMovement;
		KeyboardEventHandler.A_Keyup -= ZeroTurn;
		KeyboardEventHandler.D_Keyup -= ZeroTurn;
	}
	
	void TurnLeft(){
		tank.RotateTank (-1f);
	}

	void TurnRight(){
		tank.RotateTank (1f);
	}

	void ZeroTurn(){
		tank.RotateTank (0f);
	}

	void Forward(){
		tank.SetPower (1f);
	}

	void BackWard(){
		tank.SetPower (-1f);
	}

	void ZeroMovement(){
		tank.SetPower (0f);
	}
}
