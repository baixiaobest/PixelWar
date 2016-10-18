using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {
	public Camera cam;
	public GameObject ActiveWeapon;
	public float scopeFOV = 40;
	public float unscopeFOV = 60;

	// Use this for initialization
	void Start () {
		KeyboardEventHandler.Fire1_Keydown += TriggerPressed;
		KeyboardEventHandler.Fire1_Keyup += TriggerUp;
		KeyboardEventHandler.Fire2_Keydown += UseWeaponScope;
		KeyboardEventHandler.Fire2_Keyup += NotUseWeaponScope;
	}

	private void TriggerPressed(){
		ActiveWeapon.GetComponent<Weapon> ().TriggerPressed();
	}

	private void TriggerUp(){
		ActiveWeapon.GetComponent<Weapon> ().TriggerUp();
	}

	private void UseWeaponScope(){
		ActiveWeapon.GetComponent<Weapon> ().UseScope ();
		cam.fieldOfView = scopeFOV;
	}
	private void NotUseWeaponScope(){
		ActiveWeapon.GetComponent<Weapon> ().UnScope ();
		cam.fieldOfView = unscopeFOV;
	}
}
