using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AirplaneCamera : MonoBehaviour {
	public GameObject vehicle;
	public Transform LookatPos;
	public float lerp=0.5f;

	private Vector3 relativePos;
	private Camera cam;
	private AudioListener listener;

	void Start () {
		cam = GetComponent<Camera> ();
		listener = GetComponent<AudioListener> ();
		transform.root.gameObject.GetComponent<ControlRegistration> ().RegisterControl += EnableCamera;
		transform.root.gameObject.GetComponent<ControlRegistration> ().UnregisterControl += DisableCamera;

		transform.parent = null;
		relativePos = vehicle.transform.InverseTransformPoint(transform.position);
	}

	void Update () {
		if (vehicle == null) {
			Destroy (gameObject);
			return;
		}
		transform.position = vehicle.transform.TransformPoint (relativePos);
		transform.forward = Vector3.Lerp (transform.forward, LookatPos.position - transform.position, lerp * Time.deltaTime);

	}

	void EnableCamera(KeyboardEventHandler keyboard){
		cam.enabled = true;
		listener.enabled = true;
	}

	void DisableCamera(KeyboardEventHandler keyboard){
		cam.enabled = false;
		listener.enabled = false;
	}
}
