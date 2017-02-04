using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {
	public Camera cam;
	public GameObject ActiveWeapon;
	public float scopeFOV = 40;
	public float unscopeFOV = 60;

	private KeyboardEventHandler keyboard;

	// Use this for initialization
	void Start () {
		keyboard = GetComponent<KeyboardEventHandler> ();
		keyboard.Fire1_Keydown += TriggerPressed;
		keyboard.Fire1_Keyup += TriggerUp;
		keyboard.Fire2_Keydown += UseWeaponScope;
		keyboard.Fire2_Keyup += NotUseWeaponScope;
	}

	protected void TriggerPressed(){
		ActiveWeapon.GetComponent<Weapon> ().TriggerPressed();
	}

	protected void TriggerUp(){
		ActiveWeapon.GetComponent<Weapon> ().TriggerUp();
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
