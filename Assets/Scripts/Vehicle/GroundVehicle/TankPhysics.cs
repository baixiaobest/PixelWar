using UnityEngine;
using System.Collections;

public class TankPhysics : MonoBehaviour {
	public float maximumSpeed;
	public float maximumAcceleration;
	public float skidReduction;
	public float maxEngineBreakForce;
	public float engineBreakCoe;
	public float maxTankTurnSpeed;

	enum MovementMode {Ground, Air};

	private Rigidbody rigid;
	private float acceleration=0;
	private MovementMode movementMode = MovementMode.Ground;
	private float tankTurnSpeed=0;

	void Start(){
		rigid = GetComponent<Rigidbody> ();
	}

	void Update(){
		if (movementMode == MovementMode.Ground) {
			// engine acceleration + acceleration due to gravity
			rigid.velocity += (acceleration +  9.8f * Vector3.Dot(-Vector3.up, transform.forward) ) * Time.deltaTime * transform.forward;
			Vector3 antiSkid = Vector3.Dot (rigid.velocity, transform.right) * skidReduction * Time.deltaTime * (-transform.right);
			rigid.velocity += antiSkid;
			if (rigid.velocity.magnitude > maximumSpeed)
				rigid.velocity = rigid.velocity.normalized * maximumSpeed;

			// sideway skid friction
			Vector3 skidFriction = Vector3.Dot (Physics.gravity, transform.right) * (-transform.right);
			rigid.AddForce (skidFriction);

			// engine break
			if (acceleration == 0) {
				float engineBreakForce = Vector3.Dot(rigid.velocity, transform.forward) * engineBreakCoe;
				if (engineBreakForce > maxEngineBreakForce)
					engineBreakForce = maxEngineBreakForce;
				Vector3 engineBreakForceVec = engineBreakForce * (-transform.forward);
				rigid.AddForce (engineBreakForceVec);
			}

			// turn the tank
			transform.RotateAround (transform.position, transform.up, tankTurnSpeed * Time.deltaTime);
		}
	}

	public void SetPower(float percentage){
		percentage = Mathf.Clamp (percentage, -1f, 1f);
		acceleration = percentage * maximumAcceleration;
	}

	public void RotateTank(float percentage){
		percentage = Mathf.Clamp (percentage, -1f, 1f);
		tankTurnSpeed = percentage * maxTankTurnSpeed;
	}

	void OnCollisionStay(Collision col){
		movementMode = MovementMode.Ground;
	}

	void OnCollisionExit(Collision col){
		movementMode = MovementMode.Air;
	}
}
