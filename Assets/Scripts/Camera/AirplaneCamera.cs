using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AirplaneCamera : BaseCamera {
	public GameObject Camera;
	public Transform LookatPos;
	public float lerp=0.5f;

	private GameObject vehicle;
	private Vector3 relativePos;
	private Camera cam;
	private AudioListener listener;
	private KeyboardEventHandler keyboard;

	void Start(){
		cam = Camera.GetComponent<Camera> ();
		listener = Camera.GetComponent<AudioListener> ();
		GetComponent<ControlRegistration> ().RegisterControl += GetKeyboard;
		vehicle = gameObject;
		Camera.transform.parent = null;
		relativePos = vehicle.transform.InverseTransformPoint(Camera.transform.position);
		Disable ();
	}

	void Update () {
		Camera.transform.position = vehicle.transform.TransformPoint (relativePos);
		Camera.transform.forward = Vector3.Lerp (Camera.transform.forward, LookatPos.position - Camera.transform.position, lerp * Time.deltaTime);

		// disable camera if client disconnect
		if (keyboard == null) {
			Disable ();
		}
	}

	void GetKeyboard(KeyboardEventHandler key){
		keyboard = key;
	}

	void OnDestroy(){
		Destroy (Camera);
	}

	override public void Enable(){
		cam.enabled = true;
		listener.enabled = true;
	}

	override public void Disable(){
		cam.enabled = false;
		listener.enabled = false;
	}
}
