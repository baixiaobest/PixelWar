using UnityEngine;
using System.Collections;

public class AIWeaponControl : MonoBehaviour {
	public GameObject activeWeapon;


	private float timer=0;

	void Update () {
		if (timer > 0)
			timer -= Time.deltaTime;
		if (timer <= 0) {
			activeWeapon.GetComponent<Weapon> ().TriggerUp ();
		}
	}

	public void Fire(){
		activeWeapon.GetComponent<Weapon> ().Fire ();
	}

	public void FireForTime(float time){
		timer = time;
		activeWeapon.GetComponent<Weapon> ().TriggerPressed ();
	}
}
