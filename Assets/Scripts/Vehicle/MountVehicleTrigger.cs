using UnityEngine;
using System.Collections;

public class MountVehicleTrigger : MonoBehaviour {
	public Camera vehicleCamera;
	public AudioListener vehicleAudioListener;

	private GameObject player;            // player in range
	private GameObject vehicleSeats;      // player mounted the vehicle
	private bool playerInVehicle = false;
	private bool playerInTrigger = false;

	void Start () {
		//vehicleCamera = this.transform.parent.GetComponentInChildren<Camera> ();
		//vehicleAudioListener = this.transform.parent.GetComponentInChildren<AudioListener> ();
		transform.root.GetComponent<KeyboardEventHandler>().F_Keydown += MountDismountVehicle;
	}

	public void MountDismountVehicle(){
		if (playerInTrigger && !playerInVehicle) {
			MountVehicle ();
		}else if (playerInVehicle) {
			DismountVehicle ();
		}
	}

	public void MountVehicle(){
		if (vehicleSeats != null)
			return;
		playerInVehicle = true;
		player.SetActive (false);
		vehicleCamera.enabled = true;
		vehicleAudioListener.enabled = true;
		vehicleSeats = player;
//		transform.parent.GetComponent<ControlRegistration> ().Register ();
	}

	public void DismountVehicle(){
		if (vehicleSeats == null)
			return;
		playerInVehicle = false;
		vehicleSeats.SetActive (true);
		vehicleSeats.transform.position = transform.position;
		vehicleSeats.GetComponent<PlayerMovement> ().SetMovementMode (PlayerMovement.MovementMode.Air);
		vehicleCamera.enabled = false;
		vehicleAudioListener.enabled = false;
		vehicleSeats = null;
		transform.parent.GetComponent<ControlRegistration> ().Unregister ();
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
