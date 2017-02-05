using UnityEngine;
using System.Collections;

public class ControlRegistration : MonoBehaviour {
	public delegate void RegisterCallback(KeyboardEventHandler keyboard);
	public event RegisterCallback RegisterControl;
	public event RegisterCallback UnregisterControl;

	private KeyboardEventHandler m_keyboard;
	public void Register(KeyboardEventHandler keyboard){
		m_keyboard = keyboard;
		if (RegisterControl != null)
			RegisterControl (keyboard);
	}

	public void Unregister(){
		if (UnregisterControl != null)
			UnregisterControl (m_keyboard);
	}
}
