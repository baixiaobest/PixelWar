using UnityEngine;
using System.Collections;

public class AutoPilot : MonoBehaviour {
	public float KpPitch;
	public float KdPitch;
	public float KiPitch;
	public float KpRoll;
	public float KdRoll;
	public float KpAltitudeToClimbRate;
	public float KiAltitudeToClimbRate;
	public float KpClimbRateToPitch;

	public float MAX_CLIMB_RATE=15f;              // maximum climb rate in altitude mode
	public float MAX_CLIMB_PITCH=45f;
	public float PITCH_INTEGRAL_MAX = 0.05f;  // integral of pitch in unit of degrees * second
	public float MAX_CLIMB_RATE_INTEGRAL = 5f;

	public enum AutopilotMode{DISABLED, ATTITUDE_FLAT, ALTITUDE_HOLD};

	private static float ROLL_SMALL_ANGLE = 30f; // within this angle, roll is considered small

	private AirplanePhysics plane;
	private AutopilotMode autopilotMode = AutopilotMode.DISABLED;
	private Rigidbody rigid;
	private float pitch=0;
	private float roll=0;
	private float yaw=0;
	private float climbRate = 0;

	private float pitchIntegral = 0f;
	private float climbRateIntegral = 0f;
	private float desiredAltitude = 0f;

	void Start(){
		plane = GetComponent<AirplanePhysics> ();
		rigid = GetComponent<Rigidbody> ();
		DebugGUI.DebugGUICallback += debugGUI;
	}

	void FixedUpdate(){
		CalculateAttitude ();
		switch (autopilotMode) {
		case AutopilotMode.DISABLED:
			break;
		case AutopilotMode.ATTITUDE_FLAT:
			AttitudeControl (0,0,ROLL_SMALL_ANGLE);
			break;
		case AutopilotMode.ALTITUDE_HOLD:
			AltitudeControl (desiredAltitude);
			break;
		}
	}

	public void EngageAutopilot(AutopilotMode mode){
		pitchIntegral = 0;
		climbRateIntegral = 0;
		autopilotMode = mode;
		if (autopilotMode == AutopilotMode.ALTITUDE_HOLD) {
			desiredAltitude = transform.position.y;
		}
	}

	public AutopilotMode GetAutopilotMode(){
		return autopilotMode;
	}

	private void CalculateAttitude(){
		// pitch calculation
		pitch = Vector3.Angle (transform.forward, Physics.gravity) - 90f;


		//roll calculation
		Vector3 rightHorizontalVec = Vector3.Cross (transform.forward, Physics.gravity).normalized;
		Vector3 forwardHorizontalVec = Vector3.Cross (Physics.gravity, rightHorizontalVec).normalized;

		roll = Vector3.Angle (rightHorizontalVec, transform.right);
		Vector3 rightHorizontalCrossRight = Vector3.Cross (rightHorizontalVec, transform.right);
		float rollSign = Vector3.Dot (rightHorizontalCrossRight, forwardHorizontalVec);
		if (rollSign < 0)
			roll = -roll;

		// yaw calculation
		yaw = Vector3.Angle (forwardHorizontalVec, Vector3.forward);

		climbRate = rigid.velocity.y;
	}

	private void AttitudeControl(float desiredPitch, float desiredRoll, float rollSmallAngle){
		float rollError = desiredRoll-roll;
		float rollVelocity = Mathf.Rad2Deg * rigid.angularVelocity.z;
		float rollPercentage = rollError * KpRoll - rollVelocity * KdRoll;
		plane.SetAileron (rollPercentage);

		if (Mathf.Abs (roll) < rollSmallAngle) {
			float pitchError = -(desiredPitch - pitch);
			float pitchVelocity = Mathf.Rad2Deg * rigid.angularVelocity.x;
			pitchIntegral = Mathf.Clamp (pitchIntegral + KiPitch * pitchError * Time.fixedDeltaTime, -PITCH_INTEGRAL_MAX, PITCH_INTEGRAL_MAX);
			float pitchPercentage = pitchError * KpPitch - pitchVelocity * KdPitch + pitchIntegral;
			plane.SetElevator (pitchPercentage);
		}
	}

	private void AltitudeControl(float desiredAltitude){

		if (Mathf.Abs (roll) < ROLL_SMALL_ANGLE) {
			float altitudeError = desiredAltitude - transform.position.y;
			climbRateIntegral = Mathf.Clamp (climbRateIntegral + KiAltitudeToClimbRate * altitudeError * Time.fixedDeltaTime, -MAX_CLIMB_RATE_INTEGRAL, MAX_CLIMB_RATE_INTEGRAL);
			float desiredClimbRate = Mathf.Clamp (altitudeError * KpAltitudeToClimbRate + climbRateIntegral, -MAX_CLIMB_RATE, MAX_CLIMB_RATE);
			float climbRateError = desiredClimbRate - climbRate;
			float newPitch = Mathf.Clamp (pitch + KpClimbRateToPitch * climbRateError, -MAX_CLIMB_PITCH, MAX_CLIMB_PITCH);
			AttitudeControl (newPitch, 0, ROLL_SMALL_ANGLE);
		} else {
			AttitudeControl (0, 0, ROLL_SMALL_ANGLE);
		}
	}

	void debugGUI(){
		GUIStyle style = new GUIStyle ();
		style.fontSize = 30;
		style.alignment = TextAnchor.LowerLeft;
		GUILayout.Label ("Pitch: " + pitch, style);
		GUILayout.Label ("Roll: " + roll, style);
		GUILayout.Label ("Yaw: " + yaw, style);
		GUILayout.Label ("Altitude: " + transform.position.y, style);
		GUILayout.Label ("Desired Altitude: " + desiredAltitude, style);
		GUILayout.Label ("Climb Rate: " + climbRate, style);
	}
}
