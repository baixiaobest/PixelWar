using UnityEngine;
using System.Collections;

public class AircraftWeaponControl : MonoBehaviour {
	public GameObject[] Guns;

	private KeyboardEventHandler keyboard;
	public void Start(){
		keyboard = GetComponent<KeyboardEventHandler> ();
		GetComponent<ControlRegistration> ().RegisterControl += RegisterControl;
		GetComponent<ControlRegistration> ().UnregisterControl += UnregisterControl;
	}

	public void RegisterControl(){
		keyboard.Fire1_Keydown += TriggerPressed;
		keyboard.Fire1_Keyup += TriggerUp;
	}

	public void UnregisterControl(){
		keyboard.Fire1_Keydown -= TriggerPressed;
		keyboard.Fire1_Keyup -= TriggerUp;
	}
	
	protected void TriggerPressed(){
		for (int i = 0; i < Guns.Length; i++)
			Guns [i].GetComponent<Weapon> ().TriggerPressed ();
	}

	protected void TriggerUp(){
		for (int i = 0; i < Guns.Length; i++)
			Guns [i].GetComponent<Weapon> ().TriggerUp ();
	}
}
