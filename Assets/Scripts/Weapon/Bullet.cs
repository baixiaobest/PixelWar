using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed;
	public int damage;
	public float lifeTime;
	public float gravityEffect=1;

	private float lifeCounter = 0;
	private Rigidbody rigid;

	protected virtual void Start(){
		rigid = GetComponent<Rigidbody> ();
	}

	void Update(){
		if (lifeCounter > lifeTime)
			Destroy (gameObject);
		lifeCounter += Time.deltaTime;
		if(rigid.velocity.magnitude != 0)
			transform.forward = rigid.velocity.normalized;
	}

	void FixedUpdate(){
		Vector3 antiGravity = -Physics.gravity * (1 - gravityEffect);
		rigid.AddForce (antiGravity);
	}

	public void Fire(){
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.CompareTag ("Player") || collision.gameObject.CompareTag ("Enemy")){
			Destroy (gameObject);
		}else {
			Reflect (collision);
		}
		DealDamage (collision.gameObject, damage);
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
