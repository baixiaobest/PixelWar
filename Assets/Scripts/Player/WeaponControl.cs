using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {
	public GameObject ActiveWeapon;

	// Use this for initialization
	void Start () {
		KeyboardEventHandler.Fire1_Keydown += TriggerPressed;
		KeyboardEventHandler.Fire1_Keyup += TriggerUp;
	}

	private void TriggerPressed(){
		ActiveWeapon.GetComponent<Weapon> ().TriggerPressed();
	}

	private void TriggerUp(){
		ActiveWeapon.GetComponent<Weapon> ().TriggerUp();
	}
}
