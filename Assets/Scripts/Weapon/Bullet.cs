using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed;
	public float lifeTime;

	private float lifeCounter = 0;
	private Rigidbody rigid;

	void Start(){
		rigid = GetComponent<Rigidbody> ();
	}

	void Update(){
		if (lifeCounter > lifeTime)
			Destroy (gameObject);
		lifeCounter += Time.deltaTime;
		transform.forward = rigid.velocity.normalized;
	}

	public void Fire(){
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("Floor") || collision.gameObject.CompareTag ("Obstacle")) {
			ContactPoint contactPt = collision.contacts [0];
			Vector3 incomingVec = -transform.forward;
			// reflect incoming vector against normal of contact point
			float incomingDotnormal = Vector3.Dot (incomingVec, contactPt.normal);
			Vector3 outgoingVec = incomingVec + 2 * (contactPt.normal * incomingDotnormal - incomingVec);
			transform.position = contactPt.point;
			transform.forward = outgoingVec;

			float scale = 1f - 0.5f * incomingDotnormal;
			rigid.velocity = outgoingVec * scale * rigid.velocity.magnitude;
			float lengthScale = rigid.velocity.magnitude / speed;
			transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, lengthScale * transform.localScale.z);
		} else {
			Destroy (gameObject);
		}
	}
}
