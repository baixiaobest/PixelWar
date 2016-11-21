using UnityEngine;
using System.Collections;

public class VehicleCamera : MonoBehaviour {
	public Camera cam;
	public float sensitivity;
	private Vector3 initialPos;
	private Vector3 relativePos;

	void Start () {
		initialPos = cam.transform.position - transform.position;
		relativePos = initialPos;
		GetComponent<ControlRegistration> ().RegisterControl += RegisterControl;
		GetComponent<ControlRegistration> ().UnregisterControl += UnregisterControl;
	}

	void RegisterControl(){
		cam.transform.position = transform.position + initialPos;
		cam.transform.parent = null;
		cam.transform.up = Vector3.up;
		KeyboardEventHandler.MouseMovement += MouseMoved;
	}

	void UnregisterControl(){
		cam.transform.parent = transform;
		KeyboardEventHandler.MouseMovement -= MouseMoved;
	}
	
	// Update is called once per frame
	void Update () {
		cam.transform.position = transform.position + relativePos;
	}

	void MouseMoved(float x, float y){
		cam.transform.RotateAround (transform.position, Vector3.up, x * sensitivity);
		cam.transform.RotateAround (transform.position, cam.transform.right, -y * sensitivity);
		relativePos = cam.transform.position - transform.position;
	}
}
