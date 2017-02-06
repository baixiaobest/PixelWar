using UnityEngine;
using System.Collections;

public class GroundVehicleWeaponControl : MonoBehaviour {
	public GameObject mainGun;
	public float scopeFOV = 40;
	public float unscopeFOV = 60;

	public void Start(){
		GetComponent<ControlRegistration> ().RegisterControl += RegisterControl;
		GetComponent<ControlRegistration> ().UnregisterControl += UnregisterControl;
	}

	public void RegisterControl(KeyboardEventHandler keyboard){
		keyboard.Fire1_Key += Trigger;
	}

	public void UnregisterControl(KeyboardEventHandler keyboard){
		keyboard.Fire1_Key -= Trigger;
	}

	protected void Trigger(){
		mainGun.GetComponent<Weapon> ().Fire ();
	}
}
