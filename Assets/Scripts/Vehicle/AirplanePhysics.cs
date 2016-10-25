using UnityEngine;
using System.Collections;

public class AirplanePhysics : MonoBehaviour {
	public Transform centerOfMass;
	public Transform centerOfLift;
	public Transform centerOfTrust;
	public Transform centerOfBodyDrag;

	public float stallAngle = 45f;
	/////////////
	/// DRAG ////
	/////////////
	// when angle of attack smaller than this value, drag is constant, when larger than this,
	// drag becomes a function of angle of attack
	// this function is characterized by parameter dragFactor
	// refer to graphs at https://www.grc.nasa.gov/www/k-12/airplane/inclind.html

	public float smallAngleDragCoefficient = 4f; // constant wing drag coefficient when angle of attack under dragSamllAngle degrees
	public float dragSmallAngle = 5f;   // within this angle, wing drag coefficient is constant
	public float dragFactor = 0.5f;  // describes how fast coefficient of drag increases after small angle
	public float stallDragCoefficient = 6f;
	// body drag
	public float initialBodyDragCoefficient = 0.1f;  // describes how airspeed create drag on aircraft body
	public float bodyDragFactor=0.05f; // how fast body drag coefficient increases as angle of airflow against aircraft body increases

	////////////
	/// LIFT ///
	////////////

	// for coefficient of lift calculation
	// lift is a function of angle of attack, characterized by two parameter COLslope and COLYaxisCross
	// refer to https://www.grc.nasa.gov/www/k-12/airplane/incline.html
	public float COLslope = 10f;
	public float COLYaxisCross = 280f;
	public float stallLiftCoefficient = 140f;


	///////////////
	/// CONTROL ///
	///////////////

	public float maxThrottle=15000f;
	public float aileronTorqueCoefficient=300f;
	public float elevatorTorqueCoefficient=100f;
	public float rudderTorqueCoefficient=50f;

	private Rigidbody rigid;
	private float throttle;   // current aircraft throttle

	// these control how much of aileron,elevator,rudder and trim are set, value from -1 to 1;
	private float aileronControl=0;
	private float elevatorControl=0;
	private float rudderControl=0;
	public float elevatorTrim=0;

	// how much elevator can trim
	private static float MAX_TRIM_PERCENTAGE = 0.3f;

	// debug
	private float AOA;
	private float dragCoe;
	private float bodyDragCoe;

	void Start(){
		throttle = 0;
		Vector3 COM = centerOfMass.position - transform.position;
		rigid = GetComponent<Rigidbody> ();
		rigid.centerOfMass = COM;
		// debug
		DebugGUI.DebugGUICallback += debugGUI;
	}

	void FixedUpdate(){
		Vector3 throttleVec = throttle * centerOfTrust.forward;
		rigid.AddForceAtPosition (throttleVec, centerOfTrust.position);

		// lift
		float airflowSpeed = rigid.velocity.magnitude;
		float angleOfAttack = Vector3.Angle (rigid.velocity, centerOfLift.up) - 90f;
		float lift;
		if (Mathf.Abs (angleOfAttack) < stallAngle) {
			float liftCoefficient = COLslope * angleOfAttack + COLYaxisCross;
			lift = rigid.velocity.magnitude * liftCoefficient;
		} else {
			lift = stallLiftCoefficient;
		}
		Vector3 liftVec = centerOfLift.up * lift;
		rigid.AddForceAtPosition (liftVec, centerOfLift.position);

		// drag of wing
		float dragCoefficient;
		// different drag coefficients for stall and normal state
		if (Mathf.Abs (angleOfAttack) < stallAngle) {
			float angleExceedSmallAngle = Mathf.Clamp (Mathf.Abs (angleOfAttack) - dragSmallAngle, 0, 90);
			dragCoefficient = smallAngleDragCoefficient + dragFactor * angleExceedSmallAngle;
		} else {
			dragCoefficient = stallDragCoefficient;
		}
		float wingDrag = dragCoefficient * rigid.velocity.magnitude * rigid.velocity.magnitude;
		Vector3 wingDragVec = -rigid.velocity.normalized * wingDrag;
		rigid.AddForceAtPosition (wingDragVec, centerOfMass.position);

		// drag of aircraft body
		float airflowBodyAngle = Mathf.Min (Vector3.Angle (rigid.velocity, centerOfMass.forward), Vector3.Angle (rigid.velocity, -centerOfMass.forward));
		float bodyDragCoefficient = initialBodyDragCoefficient + airflowBodyAngle * bodyDragFactor;
		float bodyDrag = bodyDragCoefficient * rigid.velocity.magnitude * rigid.velocity.magnitude;
		Vector3 bodyDragVec = -bodyDrag * rigid.velocity.normalized;
		rigid.AddForceAtPosition (bodyDragVec, centerOfBodyDrag.position);


		Vector3 aileronTorque = aileronControl * aileronTorqueCoefficient * airflowSpeed * centerOfMass.forward;
		Vector3 elevatorTorque = (elevatorControl + MAX_TRIM_PERCENTAGE * elevatorTrim) * elevatorTorqueCoefficient * airflowSpeed * centerOfMass.right;
		Vector3 rudderTorque = rudderControl * rudderTorqueCoefficient * airflowSpeed * centerOfMass.up;
		rigid.AddTorque (aileronTorque);
		rigid.AddTorque (elevatorTorque);
		rigid.AddTorque (rudderTorque);

		// debug
		AOA = angleOfAttack;
		dragCoe = dragCoefficient;
		bodyDragCoe = bodyDragCoefficient;
		Debug.DrawRay (centerOfTrust.position, 10f * throttleVec/maxThrottle, Color.green);
		Debug.DrawRay (centerOfLift.position, liftVec / 500f, Color.green);
		Debug.DrawRay (centerOfMass.position, wingDragVec / 10f, Color.red);
		Debug.DrawRay (centerOfLift.position, rigid.velocity, Color.blue);
		Debug.DrawRay (centerOfLift.position, centerOfLift.forward * 10f, Color.yellow);
	}

	public void SetThrottle(float throttlePercentage){
		throttle = maxThrottle * Mathf.Clamp (throttlePercentage, 0, 1);
	}

	public float GetCurrentThrottlePercentage(){
		return throttle/maxThrottle;
	}

	// - roll right, + roll left
	public void SetAileron(float value){
		aileronControl = Mathf.Clamp (value, -1, 1);
	}

	// - climb up, + pitch down
	public void SetElevator(float value){
		elevatorControl = Mathf.Clamp (value, -1, 1);
	}

	// - turn left, + turn right
	public void SetRudder(float value){
		rudderControl = Mathf.Clamp (value, -1, 1);
	}

	public void SetElevatorTrim(float value){
		elevatorTrim = Mathf.Clamp (value, -1, 1);
	}

	public float GetElevatorTrim(){
		return elevatorTrim;
	}

	void debugGUI(){
		GUIStyle style = new GUIStyle ();
		style.fontSize = 30;
		GUILayout.Label ("Throttle: " + throttle/maxThrottle * 100 + "%", style);
		GUILayout.Label ("Airspeed: " + rigid.velocity.magnitude, style);
		GUILayout.Label ("Angle of Attack: "+AOA, style);
		GUILayout.Label ("Wing Drag Coefficient: " + dragCoe, style);
		GUILayout.Label ("Body Drag Coefficient: " + bodyDragCoe, style);
		GUILayout.Label ("Elevator Trim: " + elevatorTrim, style);
	}
}
