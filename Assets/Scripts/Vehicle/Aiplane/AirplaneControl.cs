using UnityEngine;
using System.Collections;

public class AirplaneControl : MonoBehaviour {

	private AirplanePhysics airplane;
	private AutoPilot autoPilot;
	private KeyboardEventHandler keyboard;

	// Use this for initialization
	void Start () {
		airplane = GetComponent<AirplanePhysics> ();
		autoPilot = GetComponent<AutoPilot> ();
		keyboard = GetComponent<KeyboardEventHandler> ();
		GetComponent<ControlRegistration> ().RegisterControl += RegisterControl;
		GetComponent<ControlRegistration> ().UnregisterControl += UnregisterControl;
	}

	public void RegisterControl(){
		keyboard.D_Right += RollRight;
		keyboard.A_Left += RollLeft;
		keyboard.S_Backward += PitchUp;
		keyboard.W_Forward += PitchDown;
		keyboard.L_Shift += ThrustUp;
		keyboard.L_Control += ThrustDown;
		keyboard.Q_Keydown += RudderLeft;
		keyboard.E_Keydown += RudderRight;
		keyboard.W_Keyup += PitchZero;
		keyboard.S_Keyup += PitchZero;
		keyboard.A_Keyup += RollZero;
		keyboard.D_Keyup += RollZero;
		keyboard.Q_Keyup += RudderZero;
		keyboard.E_Keyup += RudderZero;
		keyboard.M_Keydown += ElevatorTrimUp;
		keyboard.N_Keydown += ElevatorTrimDown;
		keyboard.O_Keydown += AutopilotFlatAttitude;
	}

	public void UnregisterControl(){
		keyboard.D_Right -= RollRight;
		keyboard.A_Left -= RollLeft;
		keyboard.S_Backward -= PitchUp;
		keyboard.W_Forward -= PitchDown;
		keyboard.L_Shift -= ThrustUp;
		keyboard.L_Control -= ThrustDown;
		keyboard.Q_Keydown -= RudderLeft;
		keyboard.E_Keydown -= RudderRight;
		keyboard.W_Keyup -= PitchZero;
		keyboard.S_Keyup -= PitchZero;
		keyboard.A_Keyup -= RollZero;
		keyboard.D_Keyup -= RollZero;
		keyboard.Q_Keyup -= RudderZero;
		keyboard.E_Keyup -= RudderZero;
		keyboard.M_Keydown -= ElevatorTrimUp;
		keyboard.N_Keydown -= ElevatorTrimDown;
		keyboard.O_Keydown -= AutopilotFlatAttitude;
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
