using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Camera_Network : MonoBehaviour {

	void Start () {
		NetworkIdentity id = transform.root.GetComponent<NetworkIdentity> ();
		if (id == null || !id.isLocalPlayer) {
			GetComponent<Camera> ().enabled = false;
			GetComponent<AudioListener> ().enabled = false;
		} else {
			GetComponent<Camera> ().enabled = true;
			GetComponent<AudioListener> ().enabled = true;
		}
	}
}
