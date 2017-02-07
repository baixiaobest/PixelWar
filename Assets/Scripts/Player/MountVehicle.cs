using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MountVehicle : NetworkBehaviour {
	public LayerMask vehicleLayer;
	public Transform raycastPoint;
	public GameObject playerCamera;
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
	public void Mount(){
		if (playerInVehicle)
			return;
		if (vehicle.GetComponent<ControlRegistration> ().Register (keyboard)) {
			GetComponent<PlayerMovement> ().Unregister ();
			GetComponent<WeaponControl> ().Unregister ();
			playerInVehicle = true;
			playerCamera.GetComponent<Camera> ().enabled = false;
			playerCamera.GetComponent<AudioListener> ().enabled = false;
			vehicle.GetComponent<BaseCamera> ().Enable ();
			CmdMount (vehicle.GetComponent<NetworkIdentity>(), GetComponent<NetworkIdentity>(), vehicle);
		}
	}

	// It does the reverse of what Mount() does.
	public void Dismount(){
		if (!playerInVehicle)
			return;
		playerInVehicle = false;
		GetComponent<PlayerMovement> ().Register ();
		GetComponent<WeaponControl> ().Register ();
		vehicle.GetComponent<ControlRegistration> ().Unregister();
		playerCamera.GetComponent<Camera> ().enabled = true;
		playerCamera.GetComponent<AudioListener> ().enabled = true;
		vehicle.GetComponent<BaseCamera> ().Disable ();
		CmdDismount (vehicle.GetComponent<NetworkIdentity>(), GetComponent<NetworkIdentity>(), vehicle);
	}

	// Command send to server.
	// It allows client to claim authority over the vehicle
	// It also set up a synced variable that tell other clients that the vehicle is occupied
	[Command]
	public void CmdMount(NetworkIdentity vehicleID, NetworkIdentity playerID, GameObject myVehicle){
		vehicleID.AssignClientAuthority (playerID.connectionToClient);    // allow client to control and sync vehicle
		myVehicle.GetComponent<ControlRegistration> ().RegisterToServer ();  // disallow other clients to control
	}

	// it release client authority, so server regain authority over vehicle
	// also, all controls to vehicle are unregistered
	[Command]
	public void CmdDismount(NetworkIdentity vehicleID, NetworkIdentity playerID, GameObject myVehicle){
		vehicleID.RemoveClientAuthority (playerID.connectionToClient);
		myVehicle.GetComponent<ControlRegistration> ().UnregisterToServer ();
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
