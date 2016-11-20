using UnityEngine;
using System.Collections;

public class MountVehicleTrigger : MonoBehaviour {
	private Camera vehicleCamera;
	private AudioListener vehicleAudioListener;
	private GameObject player;
	private bool playerInVehicle = false;
	private bool playerInTrigger = false;

	void Start () {
		vehicleCamera = this.transform.parent.GetComponentInChildren<Camera> ();
		vehicleAudioListener = this.transform.parent.GetComponentInChildren<AudioListener> ();
		KeyboardEventHandler.F_Keydown += MountDismountVehicle;
	}

	void MountDismountVehicle(){
		if (playerInTrigger && !playerInVehicle) {
			playerInVehicle = true;
			player.SetActive (false);
			vehicleCamera.enabled = true;
			vehicleAudioListener.enabled = true;
			if (transform.parent.CompareTag ("Airplane")) {
				AirplaneSetActive (true);
			}
		}else if (playerInVehicle) {
			playerInVehicle = false;
			player.SetActive (true);
			player.transform.position = transform.position;
			player.GetComponent<PlayerMovement> ().SetMovementMode (PlayerMovement.MovementMode.Air);
			vehicleCamera.enabled = false;
			vehicleAudioListener.enabled = false;
			if (transform.parent.CompareTag ("Airplane")) {
				AirplaneSetActive (false);
			}
		}
	}

	void AirplaneSetActive(bool enabled){
		GameObject airplane = transform.parent.gameObject;
		if (enabled) {
			airplane.GetComponent<AircraftWeaponControl> ().RegisterControl();
			airplane.GetComponent<AirplaneControl> ().RegisterControl();
		} else {
			airplane.GetComponent<AircraftWeaponControl> ().UnregisterControl();
			airplane.GetComponent<AirplaneControl> ().UnregisterControl();
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
