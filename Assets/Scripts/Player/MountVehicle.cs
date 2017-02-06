using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MountVehicle : NetworkBehaviour {
	public LayerMask vehicleLayer;
	public Transform raycastPoint;
	public float raycastRange=3f;

	private GameObject vehicle;
	private KeyboardEventHandler keyboard;
	private bool playerInVehicle;
	private bool vehicleInRange;

	void Start(){
		keyboard = GetComponent<KeyboardEventHandler> ();
		keyboard.F_Keydown += MountDismount;
		playerInVehicle = false;
	}

	void MountDismount(){
		if (vehicleInRange && !playerInVehicle) {
			Mount ();
		} else if(playerInVehicle){
			Dismount ();
		}
	}

	// This function let control functions in vehicle to register itself with KeyboardEventHandler
	// It also unregister controls for playermovement and player weapon
	void Mount(){
		if (vehicle.GetComponent<ControlRegistration> ().Register (keyboard)) {
			GetComponent<PlayerMovement> ().Unregister ();
			GetComponent<WeaponControl> ().Unregister ();
			playerInVehicle = true;
			CmdMount (vehicle.GetComponent<NetworkIdentity>(), GetComponent<NetworkIdentity>());
		}
	}

	// It does the reverse of what Mount() does.
	void Dismount(){
		playerInVehicle = false;
		GetComponent<PlayerMovement> ().Register ();
		GetComponent<WeaponControl> ().Register ();
		vehicle.GetComponent<ControlRegistration> ().Unregister();
		CmdDismount (vehicle.GetComponent<NetworkIdentity>(), GetComponent<NetworkIdentity>());
	}

	// Command send to server.
	// It allows client to claim authority over the vehicle
	// It also set up a synced variable that tell other clients that the vehicle is occupied
	[Command]
	public void CmdMount(NetworkIdentity vehicleID, NetworkIdentity playerID){
		vehicleID.AssignClientAuthority (playerID.connectionToClient);    // allow client to control and sync vehicle
		vehicle.GetComponent<ControlRegistration> ().RegisterToServer ();  // disallow other clients to control
	}

	// It does the reverse of what cmdMount does
	[Command]
	public void CmdDismount(NetworkIdentity vehicleID, NetworkIdentity playerID){
		vehicleID.RemoveClientAuthority (playerID.connectionToClient);
		vehicle.GetComponent<ControlRegistration> ().UnregisterToServer ();
	}


	// Upon each fixed update, a ray is cast from raycastPoint
	// if vehicle object is hit, then we can mount that vehicle if it is not occupied
	void FixedUpdate () {
		RaycastHit info;
		if (Physics.Raycast (raycastPoint.position, raycastPoint.forward, out info, raycastRange, vehicleLayer)) {
			vehicle = info.collider.transform.root.gameObject;
			vehicleInRange = true;
		} else {
			vehicleInRange = false;
		}
	}
}
