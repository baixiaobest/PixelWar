using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NewNetworkManager : NetworkManager {

	// remove authority from non-player objects with client authority,
	// so that non-player objects will not be destroyed when client disconnects.
	public override void OnServerDisconnect(NetworkConnection conn){
		NetworkInstanceId[] clientObjects = new NetworkInstanceId[conn.clientOwnedObjects.Count];
		conn.clientOwnedObjects.CopyTo(clientObjects);

		foreach (NetworkInstanceId objId in clientObjects){
			GameObject obj = NetworkServer.FindLocalObject (objId);
			NetworkIdentity netIdentity = obj.GetComponent<NetworkIdentity>();
			 
			if (netIdentity.connectionToClient == null){
				netIdentity.RemoveClientAuthority(conn);
				ControlRegistration reg = obj.GetComponent<ControlRegistration> ();
				if (reg != null) {
					reg.Unregister ();
					reg.UnregisterToServer ();
				}
			}
		}

		base.OnServerDisconnect (conn);
	}

	public override void OnClientDisconnect(NetworkConnection conn){
		Debug.Log ("Client disconnect");

		base.OnClientDisconnect (conn);
	}
}
