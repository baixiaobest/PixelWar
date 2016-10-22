using UnityEngine;
using System.Collections;

public class AircraftWeaponControl : MonoBehaviour {
	public GameObject[] Guns;


	// Use this for initialization
	void Start () {
		KeyboardEventHandler.Fire1_Keydown += TriggerPressed;
		KeyboardEventHandler.Fire1_Keyup += TriggerUp;
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
