using UnityEngine;
using System.Collections;

public class Cannon : Bullet {
	public GameObject explosion;
	public AudioClip explosionSound;

	void OnCollisionEnter(Collision col){
		GameObject effect = GameObject.Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
		effect.transform.up = col.contacts [0].normal;
		AudioSource audio = effect.GetComponent<AudioSource> ();
		audio.Play ();
		Destroy (gameObject);
	}
}
