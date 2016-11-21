using UnityEngine;
using System.Collections;

public class AircraftWeaponControl : MonoBehaviour {
	public GameObject[] Guns;

	public void Start(){
		GetComponent<ControlRegistration> ().RegisterControl += RegisterControl;
		GetComponent<ControlRegistration> ().UnregisterControl += UnregisterControl;
	}

	public void RegisterControl(){
		KeyboardEventHandler.Fire1_Keydown += TriggerPressed;
		KeyboardEventHandler.Fire1_Keyup += TriggerUp;
	}

	public void UnregisterControl(){
		KeyboardEventHandler.Fire1_Keydown -= TriggerPressed;
		KeyboardEventHandler.Fire1_Keyup -= TriggerUp;
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
