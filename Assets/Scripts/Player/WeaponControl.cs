﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WeaponControl : NetworkBehaviour {
	public Camera cam;
	public GameObject ActiveWeapon;
	public GameObject bullet;
	public float scopeFOV = 40;
	public float unscopeFOV = 60;

	private KeyboardEventHandler keyboard;
	// Use this for initialization
	void Start () {
		keyboard = GetComponent<KeyboardEventHandler> ();
		Register ();
	}

	public void Register(){
		keyboard.Fire1_Key += Trigger;
		keyboard.Fire2_Keydown += UseWeaponScope;
		keyboard.Fire2_Keyup += NotUseWeaponScope;
	}

	public void Unregister(){
		keyboard.Fire1_Key -= Trigger;
		keyboard.Fire2_Keydown -= UseWeaponScope;
		keyboard.Fire2_Keyup -= NotUseWeaponScope;
	}

	[Command]
	public void CmdSpawnBullet(Vector3 position, Vector3 forward, Quaternion rotation){
		GameObject newBullet = Instantiate (bullet, position, rotation) as GameObject;
//		newBullet.GetComponent<Rigidbody> ().velocity = newBullet.transform.forward * 10f;
		NetworkServer.Spawn (newBullet);
	}

	public void SetBullet(GameObject bulletPrefeb){
		//bullet = bulletPrefeb;
	}

	protected virtual void Trigger(){
		ActiveWeapon.GetComponent<Weapon> ().Fire ();
	}

	protected void UseWeaponScope(){
		ActiveWeapon.GetComponent<Weapon> ().UseScope ();
		cam.fieldOfView = scopeFOV;
	}
	protected void NotUseWeaponScope(){
		ActiveWeapon.GetComponent<Weapon> ().UnScope ();
		cam.fieldOfView = unscopeFOV;
	}
}
