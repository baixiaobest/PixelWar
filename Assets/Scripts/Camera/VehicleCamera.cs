using UnityEngine;
using System.Collections;

public class VehicleCamera : MonoBehaviour {
	public Camera cam;
	public float sensitivity;
	public Transform cameraLookat;
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
		cam.transform.forward = (cameraLookat.position - cam.transform.position);
	}

	void MouseMoved(float x, float y){
//		cam.transform.RotateAround (cam.transform.position, Vector3.up, x * sensitivity);
//		cam.transform.RotateAround (cam.transform.position, cam.transform.right, -y * sensitivity);
		relativePos = Quaternion.AngleAxis(x*sensitivity, Vector3.up) * relativePos;
		relativePos = Quaternion.AngleAxis (-y * sensitivity, cam.transform.right) * relativePos;
	}
}
