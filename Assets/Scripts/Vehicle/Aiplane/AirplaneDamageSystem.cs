using UnityEngine;
using System.Collections;

public class AirplaneDamageSystem : MonoBehaviour {
	public float loseControlHealth; // player loose control at this health
	public float minCollisionSpeed; // collision higher than this speed will damage the airplane
	public float collisionDamageInterval;
	public int collisionDamage; // amount of damage when collision higher than min collision speed
	public LayerMask collisionLayer; // airplane reduces health when it collides with this layer
	public MountVehicleTrigger mountTrigger;
	public GameObject explosionEffect;

	private float lastTimeDamage;
	private bool destroyAirplane = false;
	private Health health;
	private AirplanePhysics planePhy;

	void Start(){
		lastTimeDamage = -collisionDamageInterval;
		health = GetComponent<Health> ();
		planePhy = GetComponent<AirplanePhysics> ();
	}

	void Update () {
		if (destroyAirplane) {
			Destroy (gameObject);
			return;
		}

		// disable control of the aircraft if aircraft is lower than lose control health
		if(health.GetHealth() < loseControlHealth){
			planePhy.SetThrottle (0);
			planePhy.SetActiveThrottle (false);
		}
	}

	void OnCollisionEnter(Collision col){
		if (((1 << col.gameObject.layer) & collisionLayer) != 0) {
			// deal damage to aircraft due to collision
			if (Time.time - lastTimeDamage > collisionDamageInterval 
			 && Mathf.Abs (Vector3.Dot (col.relativeVelocity, col.contacts [0].normal)) >= minCollisionSpeed) 
			{
				health.TakeDamage (collisionDamage);
				lastTimeDamage = Time.time;
			}

			// explode the aircraft if it has health of 0
			// and it hits the collider
			// So that airplane does not explode in the air
			// Also, dismount player
			if (health.GetHealth () == 0 && !destroyAirplane) {
				GameObject effect = Instantiate (explosionEffect, transform.position, Quaternion.identity) as GameObject;
				effect.GetComponent<AudioSource> ().Play ();
				mountTrigger.DismountVehicle ();
				GetComponent<ControlRegistration> ().Unregister ();
				destroyAirplane = true;
			}
		}
	}
}
