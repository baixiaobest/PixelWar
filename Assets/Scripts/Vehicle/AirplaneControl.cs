using UnityEngine;
using System.Collections;

public class AirplaneControl : MonoBehaviour {

	private float thrust;
	private AirplanePhysics airplane;

	// Use this for initialization
	void Start () {
		airplane = GetComponent<AirplanePhysics> ();
		KeyboardEventHandler.D_Right += RollRight;
		KeyboardEventHandler.A_Left += RollLeft;
		KeyboardEventHandler.S_Backward += PitchUp;
		KeyboardEventHandler.W_Forward += PitchDown;
		KeyboardEventHandler.L_Shift += ThrustUp;
		KeyboardEventHandler.L_Control += ThrustDown;
		KeyboardEventHandler.W_Keyup += PitchZero;
		KeyboardEventHandler.S_Keyup += PitchZero;
		KeyboardEventHandler.A_Keyup += RollZero;
		KeyboardEventHandler.D_Keyup += RollZero;
	}

	private void ThrustUp(){
		thrust += 0.1f;
		if (thrust > 1)
			thrust = 1;
		airplane.SetThrust (thrust);
	}

	private void ThrustDown(){
		thrust -= 0.1f;
		if (thrust < 0)
			thrust = 0;
		airplane.SetThrust (thrust);
	}

	private void RollRight(){
		airplane.SetAileron (-1f);
	}

	private void RollLeft(){
		airplane.SetAileron (1f);
	}

	private void RollZero(){
		airplane.SetAileron (0f);
	}

	private void PitchUp(){
		airplane.SetElevator (-1f);
	}

	private void PitchDown(){
		airplane.SetElevator (1f);
	}

	private void PitchZero(){
		airplane.SetElevator (0f);
	}

}
