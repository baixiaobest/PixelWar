using UnityEngine;
using System.Collections;

public class AirplanePhysics : MonoBehaviour {
	public Transform centerOfMass;
	public Transform centerOfLift;
	public Transform centerOfTrust;

	public float stallAngle = 45f;
	/////////////
	/// DRAG ////
	/////////////
	// when angle of attack smaller than this value, drag is constant, when larger than this,
	// drag becomes a function of angle of attack
	// this function is characterized by parameter dragFactor
	// refer to graphs at https://www.grc.nasa.gov/www/k-12/airplane/inclind.html

	public float smallAngleDragCoefficient = 4f; // drag coefficient under certain degrees
	public float dragSmallAngle = 5f; 
	public float dragFactor = 0.5f;  // describes how fast coefficient of drag increases after small angle
	public float stallDragCoefficient = 6f;

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

	public float maxThrust;
	public float aileronTorqueCoefficient;
	public float elevatorTorqueCoefficient;
	public float rudderTorqueCoefficient;

	private Rigidbody rigid;
	private float thrust;

	// these control how much of aileron is open, value from -1 to 1;
	private float aileronControl=0;
	private float elevatorControl=0;
	private float rudderControl=0;

	// debug
	private float AOA;
	private float dragCoe;

	void Start(){
		thrust = 0;
		Vector3 COM = centerOfMass.position - transform.position;
		rigid = GetComponent<Rigidbody> ();
		rigid.centerOfMass = COM;
	}

	void FixedUpdate(){
		Vector3 thrustVec = thrust * centerOfTrust.forward;
		rigid.AddForceAtPosition (thrustVec, centerOfTrust.position);

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

		// drag
		float dragCoefficient;
		// different drag coefficients for stall and normal state
		if (Mathf.Abs (angleOfAttack) < stallAngle) {
			float angleExceedSmallAngle = Mathf.Clamp (Mathf.Abs (angleOfAttack) - dragSmallAngle, 0, 90);
			dragCoefficient = smallAngleDragCoefficient + dragFactor * angleExceedSmallAngle;
		} else {
			dragCoefficient = stallDragCoefficient;
		}

		float drag = dragCoefficient * rigid.velocity.magnitude * rigid.velocity.magnitude;
		Vector3 dragVec = -rigid.velocity.normalized * drag;
		rigid.AddForceAtPosition (dragVec, centerOfMass.position);

		Vector3 aileronTorque = aileronControl * aileronTorqueCoefficient * airflowSpeed * transform.forward;
		Vector3 elevatorTorque = elevatorControl * elevatorTorqueCoefficient * airflowSpeed * transform.right;
		Vector3 rudderTorque = rudderControl * rudderTorqueCoefficient * airflowSpeed * transform.up;
		rigid.AddTorque (aileronTorque);
		rigid.AddTorque (elevatorTorque);
		rigid.AddTorque (rudderTorque);

		// debug
		AOA = angleOfAttack;
		dragCoe = dragCoefficient;
		Debug.DrawRay (centerOfTrust.position, 10f * thrustVec/maxThrust, Color.green);
		Debug.DrawRay (centerOfLift.position, liftVec/500f, Color.green);
		Debug.DrawRay (centerOfMass.position, dragVec/10f, Color.red);
	}

	public void SetThrust(float thrustPercentage){
		thrust = maxThrust * thrustPercentage;
	}

	// + roll right, - roll left
	public void SetAileron(float value){
		aileronControl = value;
	}

	// + climb up, - pitch down
	public void SetElevator(float value){
		elevatorControl = value;
	}

	// + turn left, - turn right
	public void SetRudder(float value){
		rudderControl = value;
	}

	void OnGUI(){
		GUIStyle style = new GUIStyle ();
		style.fontSize = 30;
		GUILayout.Label ("Thrust: " + thrust/maxThrust * 100 + "%", style);
		GUILayout.Label ("Angle of Attack: "+AOA, style);
		GUILayout.Label ("Drag Coefficient: " + dragCoe, style);
	}
}
