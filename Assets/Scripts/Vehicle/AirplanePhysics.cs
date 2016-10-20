using UnityEngine;
using System.Collections;

public class AirplanePhysics : MonoBehaviour {
	public Transform centerOfMass;
	public Transform centerOfLift;
	public Transform centerOfTrust;
	public Transform leftAileron;
	public Transform rightAileron;
	public Transform elevator;
	public Transform rudder;
	public float coefficientOfDrag = 0.1f;
	// for coefficient of lift calculation
	public float COLslope = 0.1f;
	public float COLYaxisCross = 0.5f;

	public float maxThrust;
	public float aileronTorqueCoefficient;
	public float elevatorTorqueCoefficient;
	public float rudderTorqueCoefficient;



	private Vector3 COM;
	private Vector3 COT;
	private Vector3 COL;
	private Rigidbody rigid;
	private Vector3 thrustVec;

	// these control how much of aileron is open, value from -1 to 1;
	private float aileronControl=0;
	private float elevatorControl=0;
	private float rudderControl=0;

	void Start(){
		thrustVec = new Vector3 (0, 0, 0);
		COM = centerOfMass.position - transform.position;
		COT = centerOfTrust.position - transform.position;
		COL = centerOfLift.position - transform.position;
		rigid = GetComponent<Rigidbody> ();
		rigid.centerOfMass = COM;
	}

	void FixedUpdate(){
		rigid.AddForceAtPosition (thrustVec, COT);

		float airflowSpeed = Vector3.Dot (rigid.velocity, centerOfLift.forward);
		Vector3 airflow = centerOfLift.forward * airflowSpeed;
		float angleOfAttack = Vector3.Angle (airflow, centerOfLift.up) - 90f;
		float liftCoefficient = COLslope * angleOfAttack + COLYaxisCross;
		float lift = rigid.velocity.magnitude * liftCoefficient;
		Vector3 liftVec = centerOfLift.up * lift;
		rigid.AddForceAtPosition (liftVec, COL);

		float drag = coefficientOfDrag * rigid.velocity.magnitude * rigid.velocity.magnitude;
		Vector3 dragVec = -rigid.velocity.normalized * drag;
		rigid.AddForceAtPosition (dragVec, COM);

		Vector3 aileronTorque = aileronControl * aileronTorqueCoefficient * airflowSpeed * transform.forward;
		Vector3 elevatorTorque = elevatorControl * elevatorTorqueCoefficient * airflowSpeed * transform.right;
		Vector3 rudderTorque = rudderControl * rudderTorqueCoefficient * airflowSpeed * transform.up;
		rigid.AddTorque (aileronTorque);
		rigid.AddTorque (elevatorTorque);
		rigid.AddTorque (rudderTorque);
	}

	public void SetThrust(float thrustPercentage){
		thrustVec = maxThrust * thrustPercentage * centerOfLift.forward;
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
}
