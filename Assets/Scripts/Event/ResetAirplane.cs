using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetAirplane : MonoBehaviour {
	public Transform resetPostion;
	public GameObject plane;

	private bool initialReset = false;
	// Use this for initialization
	void Start () {
		KeyboardEventHandler.Return_Keydown += Reset;
	}

	void Update(){
		if (!initialReset) {
			Reset ();
			initialReset = true;
		}
	}

	private void Reset(){
		plane.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		plane.transform.position = resetPostion.position;
		plane.transform.forward = resetPostion.forward;
	}
}
