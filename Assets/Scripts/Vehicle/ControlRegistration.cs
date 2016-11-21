using UnityEngine;
using System.Collections;

public class ControlRegistration : MonoBehaviour {
	public delegate void RegisterCallback();
	public event RegisterCallback RegisterControl;
	public event RegisterCallback UnregisterControl;
	
	public void Register(){
		if (RegisterControl != null)
			RegisterControl ();
	}

	public void Unregister(){
		if (UnregisterControl != null)
			UnregisterControl ();
	}
}
