using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
	public float speed;
	public int damage;
	public float lifeTime;
	public float gravityEffect=1;

	private Rigidbody rigid;

	protected virtual void Start(){
		rigid = GetComponent<Rigidbody> ();
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
		Destroy (gameObject, lifeTime);
	}
		
	void Update(){
		if(rigid.velocity.magnitude != 0)
			transform.forward = rigid.velocity.normalized;
	}
		
	void FixedUpdate(){
		Vector3 antiGravity = -Physics.gravity * (1 - gravityEffect) * rigid.mass;
		rigid.AddForce (antiGravity);
	}

	// bullet bounces off floor or obsticle, otherwise it deals damage and gets destroyed
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.CompareTag ("Floor") || collision.gameObject.CompareTag ("Obstacle")){
			Reflect (collision);
		}else {
			DealDamage (collision.gameObject, damage);
			NetworkServer.Destroy (gameObject);
		}
	}

	protected void DealDamage(GameObject collidedObject, int damageValue){
		Health health = collidedObject.GetComponent<Health> ();
		if (health != null) {
			health.TakeDamage (damageValue);
		}
	}

	private void Reflect(Collision collision){
		ContactPoint contactPt = collision.contacts [0];
		Vector3 incomingVec = -transform.forward;
		// reflect incoming vector against normal of contact point
		float incomingDotnormal = Vector3.Dot (incomingVec, contactPt.normal);
		Vector3 outgoingVec = incomingVec + 2 * (contactPt.normal * incomingDotnormal - incomingVec);
		transform.position = contactPt.point;
		transform.forward = outgoingVec;

		float scale = 1f - 0.5f * incomingDotnormal;
		speed = scale * speed;
		rigid.velocity = outgoingVec * speed;
		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, scale * transform.localScale.z);
	}
}
