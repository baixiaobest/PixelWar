using UnityEngine;
using System.Collections;

public class VehicleCamera : BaseCamera {
	public GameObject Camera;
	public Transform cameraLookat;
	public float sensitivity;
	public float unscopeFOV;
	public float scopeFOV;
	public float FOVlerp;

	private Vector3 initialPos;
	private Vector3 relativePos;  // camera relative position to vehicle (transform.position)
	private float targetFOV;
	private Camera cam;
	private AudioListener listener;
	private KeyboardEventHandler keyboard;

	void Start () {
		initialPos = Camera.transform.position - transform.position;
		relativePos = initialPos;
		targetFOV = unscopeFOV;
		cam = Camera.GetComponent<Camera> ();
		listener = Camera.GetComponent<AudioListener> ();
		GetComponent<ControlRegistration> ().RegisterControl += RegisterControl;
		GetComponent<ControlRegistration> ().UnregisterControl += UnregisterControl;
	}

	void RegisterControl(KeyboardEventHandler key){
		keyboard = key;
		keyboard.MouseMovement += MouseMoved;
		keyboard.Fire2_Keydown += Scope;
		keyboard.Fire2_Keyup += Unscope;
	}

	void UnregisterControl(KeyboardEventHandler key){
		key.MouseMovement -= MouseMoved;
		key.Fire2_Keydown -= Scope;
		key.Fire2_Keyup -= Unscope;
	}

	void Update () {
		// update position of camera
		Camera.transform.position = transform.position + relativePos;
		Camera.transform.forward = (cameraLookat.position - cam.transform.position);
		// update FOV of camera
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, FOVlerp * Time.deltaTime);

		// disable camera if client disconnects
		if (keyboard == null) {
			Disable ();
		}
	}

	void GetKeyboard(KeyboardEventHandler key){
		keyboard = key;
	}

	void MouseMoved(float x, float y){
		relativePos = Quaternion.AngleAxis(x*sensitivity, Vector3.up) * relativePos;
		relativePos = Quaternion.AngleAxis (-y * sensitivity, cam.transform.right) * relativePos;
	}

	void Scope(){
		targetFOV = scopeFOV;
	}

	void Unscope(){
		targetFOV = unscopeFOV;
	}

	override public void Enable(){
		Camera.transform.position = transform.position + initialPos;
		Camera.transform.parent = null;
		Camera.transform.up = Vector3.up;

		cam.enabled = true;
		listener.enabled = true;
	}

	override public void Disable(){
		Camera.transform.parent = transform;

		cam.enabled = false;
		listener.enabled = false;
	}
}
