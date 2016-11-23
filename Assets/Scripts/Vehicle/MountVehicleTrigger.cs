using UnityEngine;
using System.Collections;

public class MountVehicleTrigger : MonoBehaviour {
	public Camera vehicleCamera;
	public AudioListener vehicleAudioListener;
	private GameObject player;
	private bool playerInVehicle = false;
	private bool playerInTrigger = false;

	void Start () {
		//vehicleCamera = this.transform.parent.GetComponentInChildren<Camera> ();
		//vehicleAudioListener = this.transform.parent.GetComponentInChildren<AudioListener> ();
		KeyboardEventHandler.F_Keydown += MountDismountVehicle;
	}

	public void MountDismountVehicle(){
		if (playerInTrigger && !playerInVehicle) {
			playerInVehicle = true;
			player.SetActive (false);
			vehicleCamera.enabled = true;
			vehicleAudioListener.enabled = true;
			transform.parent.GetComponent<ControlRegistration> ().Register ();
		}else if (playerInVehicle) {
			playerInVehicle = false;
			player.SetActive (true);
			player.transform.position = transform.position;
			player.GetComponent<PlayerMovement> ().SetMovementMode (PlayerMovement.MovementMode.Air);
			vehicleCamera.enabled = false;
			vehicleAudioListener.enabled = false;
			transform.parent.GetComponent<ControlRegistration> ().Unregister ();
		}
	}
	
	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player")) {
			playerInTrigger = true;
			player = col.gameObject;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.CompareTag ("Player")) {
			playerInTrigger = false;
		}
	}
}
