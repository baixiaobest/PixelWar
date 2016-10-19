using UnityEngine;
using System.Collections;

public class SMG : Weapon {
	public float fireInterval = 0.1f;
	public ParticleSystem muzzleFlare;
	public GameObject bullet;
	public AudioClip fireSound;

	private float lastFireTime=0;
	private AudioSource audio;

	void Start(){
		base.Start();
		audio = GetComponent<AudioSource> ();
	}

	void Update(){
		if (Time.time - lastFireTime > fireInterval && triggerPressed)
			Fire ();
		AnimateRecoil ();
	}

	public override void Fire(){
		muzzleFlare.Stop ();
		muzzleFlare.Play ();

		GameObject newBullet = Instantiate (bullet, muzzleFlare.transform.position, Quaternion.identity) as GameObject;
		newBullet.transform.forward = muzzleFlare.transform.forward;
		newBullet.GetComponent<Bullet> ().Fire ();
		StartRecoil ();

		audio.Stop ();
		audio.clip = fireSound;
		audio.Play ();

		lastFireTime = Time.time;
	}
}
