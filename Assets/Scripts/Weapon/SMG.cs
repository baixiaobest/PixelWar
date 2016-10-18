using UnityEngine;
using System.Collections;

public class SMG : Weapon {
	public float fireInterval = 0.1f;
	public ParticleSystem muzzleFlare;
	public GameObject bullet;

	private float lastFireTime=0;
	void Update(){
		if (Time.time - lastFireTime > fireInterval && triggerPressed)
			Fire ();
	}

	public override void Fire(){
		muzzleFlare.Stop ();
		muzzleFlare.Play ();

		GameObject newBullet = Instantiate (bullet, muzzleFlare.transform.position, Quaternion.identity) as GameObject;
		newBullet.transform.forward = muzzleFlare.transform.forward;
		newBullet.GetComponent<Bullet> ().Fire ();

		lastFireTime = Time.time;
	}
}
