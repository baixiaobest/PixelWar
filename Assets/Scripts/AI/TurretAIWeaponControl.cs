using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TurretAIWeaponControl : NetworkBehaviour {
	public Transform[] weaponsMuzzles;
	public GameObject bullet;
	public float fireInterval = 0.3f;

	private float timer=0;
	private float lastTimeFire=0;

	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
			if (lastTimeFire - timer > fireInterval) {
				SpawnBullet ();
				lastTimeFire = timer;
			}
		}
	}

	public void Fire(){
		SpawnBullet ();
	}

	public void FireForTime(float time){
		SpawnBullet ();
		timer = time;
		lastTimeFire = timer;
	}

	private void SpawnBullet(){
		for(int i=0; i<weaponsMuzzles.Length; i++){
			Transform bulletTrans = weaponsMuzzles [i].transform;
			GameObject newBullet = Instantiate (bullet, bulletTrans.position, bulletTrans.rotation) as GameObject;
			NetworkServer.Spawn (newBullet);
		}
	}
}
