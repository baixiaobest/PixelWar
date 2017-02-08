using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Cannon : Bullet {
	public GameObject explosion;

	private HashSet<GameObject> ObjectsInExplosionRange;

	protected override void Start(){
		base.Start ();
		ObjectsInExplosionRange = new HashSet<GameObject> ();
	}

	// when cannon touch any collider, it explodes
	void OnCollisionEnter(Collision col){
		if (!isServer)
			return;
		GameObject effect = GameObject.Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
		effect.transform.up = col.contacts [0].normal;
		AudioSource audio = effect.GetComponent<AudioSource> ();
		audio.Play ();
		NetworkServer.Spawn (effect);
		DealExplosionDamageToObjects ();
		Destroy (gameObject);
	}

	// deal damage to all game objects within range
	// Assumption: all health script of game objects is attached to highest level of hierarchy of game object
	void DealExplosionDamageToObjects(){
		HashSet<GameObject> DamagedObjects = new HashSet<GameObject> ();
		foreach(GameObject obj in ObjectsInExplosionRange){
			float distanceToObject = (transform.position - obj.transform.position).magnitude;
			float damageRadius = GetComponent<SphereCollider> ().radius * Mathf.Max(transform.localScale.x, Mathf.Max(transform.localScale.y, transform.localScale.z));
			GameObject rootObj = obj.transform.root.gameObject;
			if (!DamagedObjects.Contains (rootObj)) {
				DealDamage (rootObj, (int)(damage * (1f - Mathf.Clamp (distanceToObject / damageRadius, 0f, 1f))));
				DamagedObjects.Add (rootObj);
			}
		}
	}

	// put all game objects within range into a hashset ObjectsInExposionRange
	void OnTriggerStay(Collider col){
		if(col.gameObject != gameObject)
			ObjectsInExplosionRange.Add (col.gameObject);
	}

	// remove game object that are certain distance larger from cannon
	void OnTriggerExit(Collider col){
		if(col.gameObject != gameObject)
			ObjectsInExplosionRange.Remove (col.gameObject);
	}
}
