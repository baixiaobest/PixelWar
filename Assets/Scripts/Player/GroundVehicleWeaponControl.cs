using UnityEngine;
using System.Collections;

public class GroundVehicleWeaponControl : MonoBehaviour {
	public GameObject mainGun;

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
		mainGun.GetComponent<Weapon> ().TriggerPressed ();
	}

	protected void TriggerUp(){
		mainGun.GetComponent<Weapon> ().TriggerUp ();
	}
}
