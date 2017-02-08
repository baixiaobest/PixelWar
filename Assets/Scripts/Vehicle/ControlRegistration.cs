using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ControlRegistration : NetworkBehaviour {
	public delegate void RegisterCallback(KeyboardEventHandler keyboard);
	public delegate void ObjectDestroyed ();

	public event RegisterCallback RegisterControl;
	public event RegisterCallback UnregisterControl;
	public event ObjectDestroyed OnObjectDestory;

	private KeyboardEventHandler m_keyboard;

	[SyncVar]
	private bool registered;  // synced variable to tell other clients that this vehicle is occupied

	void Start(){
		registered = false;
	}

	// this function allow functions that sign up for RegisterControl to be called
	// these functions will sign up itself with local KeyboardEventHandler
	public bool Register(KeyboardEventHandler keyboard){
		// somebody already registered
		if (registered)
			return false;
	
		m_keyboard = keyboard;
		if (RegisterControl != null)
			RegisterControl (m_keyboard);
		return true;
	}

	public void Unregister(){
		if (UnregisterControl != null && m_keyboard != null)
			UnregisterControl (m_keyboard);
	}

	// this is called by server.
	// server will change registered VARIABLE and sync this variable
	// to all other clients
	public void RegisterToServer(){
		registered = true;
	}

	public void UnregisterToServer(){
		registered = false;
	}

	// when vehicle is destroyed, it is only disabled on local client
	// so Disable will be called
	void OnDisable(){
		if (OnObjectDestory != null)
			OnObjectDestory ();
	}
}
