using UnityEngine;
using System.Collections;

public class GroundVehicleWeaponControl : WeaponControl {

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

	protected override void Trigger(){
		ActiveWeapon.GetComponent<Weapon> ().Fire ();
	}
}
