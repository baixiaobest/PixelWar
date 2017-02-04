using UnityEngine;
using System.Collections;

public class TankControl : MonoBehaviour {
	private TankPhysics tank;
	private KeyboardEventHandler keyboard;

	// Use this for initialization
	void Start () {
		tank = GetComponent<TankPhysics> ();
		keyboard = GetComponent<KeyboardEventHandler> ();
		GetComponent<ControlRegistration> ().RegisterControl += RegisterControl;
		GetComponent<ControlRegistration> ().UnregisterControl += UnregisterControl;
	}

	void RegisterControl(){
		keyboard.W_Forward += Forward;
		keyboard.S_Backward += BackWard;
		keyboard.A_Left += TurnLeft;
		keyboard.D_Right += TurnRight;
		keyboard.W_Keyup += ZeroMovement;
		keyboard.S_Keyup += ZeroMovement;
		keyboard.A_Keyup += ZeroTurn;
		keyboard.D_Keyup += ZeroTurn;
	}

	void UnregisterControl(){
		keyboard.W_Forward -= Forward;
		keyboard.S_Backward -= BackWard;
		keyboard.A_Left -= TurnLeft;
		keyboard.D_Right -= TurnRight;
		keyboard.W_Keyup -= ZeroMovement;
		keyboard.S_Keyup -= ZeroMovement;
		keyboard.A_Keyup -= ZeroTurn;
		keyboard.D_Keyup -= ZeroTurn;
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
