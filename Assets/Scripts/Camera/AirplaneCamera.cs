using UnityEngine;
using System.Collections;

public class AirplaneCamera : MonoBehaviour {
	public GameObject vehicle;
	public Transform LookatPos;
	public float lerp=0.5f;

	private Vector3 relativePos;

	void Start () {
		transform.parent = null;
		relativePos = vehicle.transform.InverseTransformPoint(transform.position);
	}
	

	void Update () {
		transform.position = vehicle.transform.TransformPoint (relativePos);
		transform.forward = Vector3.Lerp (transform.forward, LookatPos.position - transform.position, lerp * Time.deltaTime);
	}
}
