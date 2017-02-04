using UnityEngine;
using System.Collections;

public class GroundVehicleWeaponControl : MonoBehaviour {
	public GameObject mainGun;
	public float scopeFOV = 40;
	public float unscopeFOV = 60;

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
		mainGun.GetComponent<Weapon> ().TriggerPressed ();
	}

	protected void TriggerUp(){
		mainGun.GetComponent<Weapon> ().TriggerUp ();
	}
}
