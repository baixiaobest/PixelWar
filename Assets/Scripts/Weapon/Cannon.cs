using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cannon : Bullet {
	public GameObject explosion;

	private HashSet<GameObject> ObjectsInExplosionRange;

	protected override void Start(){
		base.Start ();
		ObjectsInExplosionRange = new HashSet<GameObject> ();
	}

	void OnCollisionEnter(Collision col){
		GameObject effect = GameObject.Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
		effect.transform.up = col.contacts [0].normal;
		AudioSource audio = effect.GetComponent<AudioSource> ();
		audio.Play ();
		DealExplosionDamageToObjects ();
		Destroy (gameObject);
	}

	void DealExplosionDamageToObjects(){
		foreach(GameObject obj in ObjectsInExplosionRange){
			float distanceToObject = (transform.position - obj.transform.position).magnitude;
			float damageRadius = GetComponent<SphereCollider> ().radius * Mathf.Max(transform.localScale.x, Mathf.Max(transform.localScale.y, transform.localScale.z));
			DealDamage ( obj, (int)(damage * (1f - Mathf.Clamp(distanceToObject / damageRadius, 0f, 1f)) ) );
		}
	}

	void OnTriggerStay(Collider col){
		if(col.gameObject != gameObject)
			ObjectsInExplosionRange.Add (col.gameObject);
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject != gameObject)
			ObjectsInExplosionRange.Remove (col.gameObject);
	}
}
