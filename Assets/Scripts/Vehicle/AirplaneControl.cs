using UnityEngine;
using System.Collections;

public class AirplaneControl : MonoBehaviour {

	private AirplanePhysics airplane;
	private AutoPilot autoPilot;

	// Use this for initialization
	void Start () {
		airplane = GetComponent<AirplanePhysics> ();
		autoPilot = GetComponent<AutoPilot> ();
		KeyboardEventHandler.D_Right += RollRight;
		KeyboardEventHandler.A_Left += RollLeft;
		KeyboardEventHandler.S_Backward += PitchUp;
		KeyboardEventHandler.W_Forward += PitchDown;
		KeyboardEventHandler.L_Shift += ThrustUp;
		KeyboardEventHandler.L_Control += ThrustDown;
		KeyboardEventHandler.Q_Keydown += RudderLeft;
		KeyboardEventHandler.E_Keydown += RudderRight;
		KeyboardEventHandler.W_Keyup += PitchZero;
		KeyboardEventHandler.S_Keyup += PitchZero;
		KeyboardEventHandler.A_Keyup += RollZero;
		KeyboardEventHandler.D_Keyup += RollZero;
		KeyboardEventHandler.Q_Keyup += RudderZero;
		KeyboardEventHandler.E_Keyup += RudderZero;
		KeyboardEventHandler.M_Keydown += ElevatorTrimUp;
		KeyboardEventHandler.N_Keydown += ElevatorTrimDown;
		KeyboardEventHandler.O_Keydown += AutopilotFlatAttitude;
	}

	private void ThrustUp(){
		float thrust = airplane.GetCurrentThrottlePercentage() + 0.01f;
		if (thrust > 1)
			thrust = 1;
		airplane.SetThrottle (thrust);
	}

	private void ThrustDown(){
		float thrust = airplane.GetCurrentThrottlePercentage() - 0.01f;
		if (thrust < 0)
			thrust = 0;
		airplane.SetThrottle (thrust);
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

	private void RudderLeft(){
		airplane.SetRudder (-1f);
	}

	private void RudderRight(){
		airplane.SetRudder (1f);
	}

	private void RudderZero(){
		airplane.SetRudder (0f);
	}

	private void ElevatorTrimUp(){
		airplane.SetElevatorTrim (airplane.GetElevatorTrim () + 0.01f);
	}

	private void ElevatorTrimDown(){
		airplane.SetElevatorTrim (airplane.GetElevatorTrim () - 0.01f);
	}

	private void AutopilotFlatAttitude(){
		if (autoPilot.GetAutopilotMode () == AutoPilot.AutopilotMode.DISABLED)
			autoPilot.EngageAutopilot (AutoPilot.AutopilotMode.ALTITUDE_HOLD);
		else
			autoPilot.EngageAutopilot (AutoPilot.AutopilotMode.DISABLED);
	}
}
