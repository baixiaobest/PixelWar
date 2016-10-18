using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	protected bool triggerPressed=false;
	public void TriggerPressed(){
		triggerPressed = true;
	}
	public void TriggerUp(){
		triggerPressed = false;
	}
	virtual public void Fire(){}
}
