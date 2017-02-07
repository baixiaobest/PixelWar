#define NETWORK_MODE 

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

#if NETWORK_MODE

public class KeyboardEventHandler : NetworkBehaviour {

	public delegate void KeyboardCallback ();
	public delegate void MouseMovementCallBack(float x, float y);

	public event KeyboardCallback W_Forward;
	public event KeyboardCallback W_Keyup;
	public event KeyboardCallback S_Backward;
	public event KeyboardCallback S_Keyup;
	public event KeyboardCallback A_Left;
	public event KeyboardCallback A_Keyup;
	public event KeyboardCallback D_Right;
	public event KeyboardCallback D_Keyup;
	public event KeyboardCallback F_Keydown;
	public event KeyboardCallback Space_Key;
	public event KeyboardCallback Space_Keydown;
	public event KeyboardCallback Q_Keydown;
	public event KeyboardCallback Q_Keyup;
	public event KeyboardCallback E_Keydown;
	public event KeyboardCallback E_Keyup;
	public event KeyboardCallback N_Keydown;
	public event KeyboardCallback M_Keydown;
	public event KeyboardCallback O_Keydown;

	public event KeyboardCallback Escape_Keydown;
	public event KeyboardCallback Return_Keydown;

	public event KeyboardCallback L_Shift;
	public event KeyboardCallback L_Control;

	public event KeyboardCallback Is_Walking;

	public event KeyboardCallback Is_Running;

	public event KeyboardCallback No_Movement;

	public event KeyboardCallback Fire1_Key;
	public event KeyboardCallback Fire1_Keydown;
	public event KeyboardCallback Fire1_Keyup;
	public event KeyboardCallback Fire2_Key;
	public event KeyboardCallback Fire2_Keydown;
	public event KeyboardCallback Fire2_Keyup;

	public event KeyboardCallback Debug_Click;

	public event KeyboardCallback Arrow_Forward;
	public event KeyboardCallback Arrow_Backward;
	public event KeyboardCallback Arrow_Left;
	public event KeyboardCallback Arrow_Right;

	public event KeyboardCallback Slot_1;
	public event KeyboardCallback Slot_2;
	public event KeyboardCallback Slot_3;
	public event KeyboardCallback Slot_4;

	public event MouseMovementCallBack MouseMovement;

	void Start(){
//		Cursor.visible = false;
//		Cursor.lockState = CursorLockMode.Locked;
		Random.InitState((int)System.DateTime.Now.Ticks);
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;


		bool movementKey = false;
		bool Lshift = false;
		if (Input.GetKey ("left shift")) {
			Lshift = true;
			if (L_Shift != null)
				L_Shift ();
		}

		if (Input.GetKey (KeyCode.LeftControl)) {
			if (L_Control != null)
				L_Control ();
		}

		if (Input.GetKey ("w")) {
			movementKey = true;
			if (W_Forward != null)
				W_Forward ();
		}
		if (Input.GetKey ("s")) {
			movementKey = true;
			if (S_Backward != null)
				S_Backward ();
		}

		if (Input.GetKey ("a")) {
			movementKey = true;
			if (A_Left != null)
				A_Left ();
		}

		if (Input.GetKey ("d")) {
			movementKey = true;
			if (D_Right != null)
				D_Right ();
		}

		if (Input.GetKeyUp ("w")) {
			if (W_Keyup != null)
				W_Keyup ();
		}
		if (Input.GetKeyUp ("s")) {
			if (S_Keyup != null)
				S_Keyup ();
		}

		if (Input.GetKeyUp ("a")) {
			if (A_Keyup != null)
				A_Keyup ();
		}

		if (Input.GetKeyUp ("d")) {
			if (D_Keyup != null)
				D_Keyup ();
		}

		if (Input.GetKeyDown ("f")) {
			if (F_Keydown != null)
				F_Keydown();
		}

		if (Input.GetKey ("space")) {
			if (Space_Key != null) {
				Space_Key ();
			}
		}

		if (Input.GetKeyDown ("space")) {
			if (Space_Keydown != null) {
				Space_Keydown ();
			}
		}

		if (Input.GetKeyDown ("q")) {
			if (Q_Keydown != null) {
				Q_Keydown ();
			}
		}

		if (Input.GetKeyUp ("q")) {
			if (Q_Keyup != null) {
				Q_Keyup ();
			}
		}

		if (Input.GetKeyDown ("e")) {
			if (E_Keydown != null) {
				E_Keydown ();
			}
		}

		if (Input.GetKeyUp ("e")) {
			if (E_Keyup != null) {
				E_Keyup ();
			}
		}

		if (Input.GetKeyDown ("n")) {
			if (N_Keydown != null) {
				N_Keydown ();
			}
		}

		if (Input.GetKeyDown ("m")) {
			if (M_Keydown != null) {
				M_Keydown ();
			}
		}

		if (Input.GetKeyDown ("o")) {
			if (O_Keydown != null) {
				O_Keydown ();
			}
		}


		if (Input.GetKeyDown ("`")) {
			if (Debug_Click != null) {
				Debug_Click ();
			}
		}

		if (Input.GetKeyDown ("escape")) {
			if (Escape_Keydown != null) {
				Escape_Keydown ();
			}
		}

		if (Input.GetKeyDown ("return")) {
			if (Return_Keydown != null) {
				Return_Keydown ();
			}
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			if (Arrow_Forward != null) {
				Arrow_Forward ();
			}
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			if (Arrow_Backward != null) {
				Arrow_Backward ();
			}
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (Arrow_Left != null) {
				Arrow_Left ();
			}
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			if (Arrow_Right != null) {
				Arrow_Right ();
			}
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if (Slot_1 != null)
				Slot_1 ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if (Slot_2 != null)
				Slot_2 ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			if (Slot_3 != null)
				Slot_3 ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			if (Slot_4 != null)
				Slot_4 ();
		}
		
		if (!movementKey) {
			if (No_Movement != null)
				No_Movement ();
		} else {
			if (Lshift) {
				if (Is_Running != null)
					Is_Running ();
			} else {
				if (Is_Walking != null)
					Is_Walking ();
			}
		}

		if (Input.GetButton ("Fire1")) {
			if (Fire1_Key != null) {
//				Cursor.visible = false;
//				Cursor.lockState = CursorLockMode.Locked;
				Fire1_Key ();
			}
		}

		if (Input.GetButtonDown ("Fire1")) {
			if (Fire1_Keydown != null)
				Fire1_Keydown ();
		}

		if (Input.GetButtonUp ("Fire1")) {
			if (Fire1_Keyup != null)
				Fire1_Keyup ();
		}

		if (Input.GetButton ("Fire2")) {
			if (Fire2_Key != null) {
				Fire2_Key ();
			}
		}

		if (Input.GetButtonDown ("Fire2")) {
			if (Fire2_Keydown != null)
				Fire2_Keydown ();
		}

		if (Input.GetButtonUp ("Fire2")) {
			if (Fire2_Keyup != null)
				Fire2_Keyup ();
		}

		if (MouseMovement != null) {
			float x = Input.GetAxis("Mouse X"); 
			float y = Input.GetAxis("Mouse Y");
			if(x != 0 || y != 0)
				MouseMovement (x, y);
		}

	}

	public void ClearFire1(){
		Fire1_Keydown = null;
	}
}

#else

public class KeyboardEventHandler : MonoBehaviour {

	public delegate void KeyboardCallback ();
	public delegate void MouseMovementCallBack(float x, float y);

	public event KeyboardCallback W_Forward;
	public event KeyboardCallback W_Keyup;
	public event KeyboardCallback S_Backward;
	public event KeyboardCallback S_Keyup;
	public event KeyboardCallback A_Left;
	public event KeyboardCallback A_Keyup;
	public event KeyboardCallback D_Right;
	public event KeyboardCallback D_Keyup;
	public event KeyboardCallback F_Keydown;
	public event KeyboardCallback Space_Key;
	public event KeyboardCallback Space_Keydown;
	public event KeyboardCallback Q_Keydown;
	public event KeyboardCallback Q_Keyup;
	public event KeyboardCallback E_Keydown;
	public event KeyboardCallback E_Keyup;
	public event KeyboardCallback N_Keydown;
	public event KeyboardCallback M_Keydown;
	public event KeyboardCallback O_Keydown;

	public event KeyboardCallback Escape_Keydown;
	public event KeyboardCallback Return_Keydown;

	public event KeyboardCallback L_Shift;
	public event KeyboardCallback L_Control;

	public event KeyboardCallback Is_Walking;

	public event KeyboardCallback Is_Running;

	public event KeyboardCallback No_Movement;

	public event KeyboardCallback Fire1_Key;
	public event KeyboardCallback Fire1_Keydown;
	public event KeyboardCallback Fire1_Keyup;
	public event KeyboardCallback Fire2_Key;
	public event KeyboardCallback Fire2_Keydown;
	public event KeyboardCallback Fire2_Keyup;

	public event KeyboardCallback Debug_Click;

	public event KeyboardCallback Arrow_Forward;
	public event KeyboardCallback Arrow_Backward;
	public event KeyboardCallback Arrow_Left;
	public event KeyboardCallback Arrow_Right;

	public event KeyboardCallback Slot_1;
	public event KeyboardCallback Slot_2;
	public event KeyboardCallback Slot_3;
	public event KeyboardCallback Slot_4;

	public event MouseMovementCallBack MouseMovement;

	void Start(){
		Cursor.visible = false;
		//		Cursor.lockState = CursorLockMode.Locked;
		Random.InitState((int)System.DateTime.Now.Ticks);
	}

	// Update is called once per frame
	void Update () {
		
		bool movementKey = false;
		bool Lshift = false;
		if (Input.GetKey ("left shift")) {
			Lshift = true;
			if (L_Shift != null)
				L_Shift ();
		}

		if (Input.GetKey (KeyCode.LeftControl)) {
			if (L_Control != null)
				L_Control ();
		}

		if (Input.GetKey ("w")) {
			movementKey = true;
			if (W_Forward != null)
				W_Forward ();
		}
		if (Input.GetKey ("s")) {
			movementKey = true;
			if (S_Backward != null)
				S_Backward ();
		}

		if (Input.GetKey ("a")) {
			movementKey = true;
			if (A_Left != null)
				A_Left ();
		}

		if (Input.GetKey ("d")) {
			movementKey = true;
			if (D_Right != null)
				D_Right ();
		}

		if (Input.GetKeyUp ("w")) {
			if (W_Keyup != null)
				W_Keyup ();
		}
		if (Input.GetKeyUp ("s")) {
			if (S_Keyup != null)
				S_Keyup ();
		}

		if (Input.GetKeyUp ("a")) {
			if (A_Keyup != null)
				A_Keyup ();
		}

		if (Input.GetKeyUp ("d")) {
			if (D_Keyup != null)
				D_Keyup ();
		}

		if (Input.GetKeyDown ("f")) {
			if (F_Keydown != null)
				F_Keydown();
		}

		if (Input.GetKey ("space")) {
			if (Space_Key != null) {
				Space_Key ();
			}
		}

		if (Input.GetKeyDown ("space")) {
			if (Space_Keydown != null) {
				Space_Keydown ();
			}
		}

		if (Input.GetKeyDown ("q")) {
			if (Q_Keydown != null) {
				Q_Keydown ();
			}
		}

		if (Input.GetKeyUp ("q")) {
			if (Q_Keyup != null) {
				Q_Keyup ();
			}
		}

		if (Input.GetKeyDown ("e")) {
			if (E_Keydown != null) {
				E_Keydown ();
			}
		}

		if (Input.GetKeyUp ("e")) {
			if (E_Keyup != null) {
				E_Keyup ();
			}
		}

		if (Input.GetKeyDown ("n")) {
			if (N_Keydown != null) {
				N_Keydown ();
			}
		}

		if (Input.GetKeyDown ("m")) {
			if (M_Keydown != null) {
				M_Keydown ();
			}
		}

		if (Input.GetKeyDown ("o")) {
			if (O_Keydown != null) {
				O_Keydown ();
			}
		}


		if (Input.GetKeyDown ("`")) {
			if (Debug_Click != null) {
				Debug_Click ();
			}
		}

		if (Input.GetKeyDown ("escape")) {
			if (Escape_Keydown != null) {
				Escape_Keydown ();
			}
		}

		if (Input.GetKeyDown ("return")) {
			if (Return_Keydown != null) {
				Return_Keydown ();
			}
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			if (Arrow_Forward != null) {
				Arrow_Forward ();
			}
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			if (Arrow_Backward != null) {
				Arrow_Backward ();
			}
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (Arrow_Left != null) {
				Arrow_Left ();
			}
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			if (Arrow_Right != null) {
				Arrow_Right ();
			}
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if (Slot_1 != null)
				Slot_1 ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if (Slot_2 != null)
				Slot_2 ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			if (Slot_3 != null)
				Slot_3 ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			if (Slot_4 != null)
				Slot_4 ();
		}

		if (!movementKey) {
			if (No_Movement != null)
				No_Movement ();
		} else {
			if (Lshift) {
				if (Is_Running != null)
					Is_Running ();
			} else {
				if (Is_Walking != null)
					Is_Walking ();
			}
		}

		if (Input.GetButton ("Fire1")) {
			if (Fire1_Key != null) {
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
				Fire1_Key ();
			}
		}

		if (Input.GetButtonDown ("Fire1")) {
			if (Fire1_Keydown != null)
				Fire1_Keydown ();
		}

		if (Input.GetButtonUp ("Fire1")) {
			if (Fire1_Keyup != null)
				Fire1_Keyup ();
		}

		if (Input.GetButton ("Fire2")) {
			if (Fire2_Key != null) {
				Fire2_Key ();
			}
		}

		if (Input.GetButtonDown ("Fire2")) {
			if (Fire2_Keydown != null)
				Fire2_Keydown ();
		}

		if (Input.GetButtonUp ("Fire2")) {
			if (Fire2_Keyup != null)
				Fire2_Keyup ();
		}

		if (MouseMovement != null) {
			float x = Input.GetAxis("Mouse X"); 
			float y = Input.GetAxis("Mouse Y");
			if(x != 0 || y != 0)
				MouseMovement (x, y);
		}

	}

	public void ClearFire1(){
		Fire1_Keydown = null;
	}
}

#endif