using UnityEngine;
using System.Collections;

public class AIWeaponControl : MonoBehaviour {
	public Weapon [] activeWeapons;


	private float timer=0;

	void Update () {
		if (timer > 0)
			timer -= Time.deltaTime;
		if (timer <= 0) {
			for(int i=0; i<activeWeapons.Length; i++)
				activeWeapons[i].Fire ();
		}
	}

	public void Fire(){
		for(int i=0; i<activeWeapons.Length; i++)
			activeWeapons[i].Fire ();
	}

	public void FireForTime(float time){
		timer = time;
		for(int i=0; i<activeWeapons.Length; i++)
			activeWeapons[i].Fire ();
	}
}
