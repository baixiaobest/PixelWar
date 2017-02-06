using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NewNetworkManager : NetworkManager {

	// this function remove authority from game objects assigned by disconnecting player
	// so that vehicle will not disappear when player disconnects
	public override void OnServerDisconnect(NetworkConnection conn){
		NetworkInstanceId[] clientObjects = new NetworkInstanceId[conn.clientOwnedObjects.Count];
		conn.clientOwnedObjects.CopyTo(clientObjects);

		foreach (NetworkInstanceId objId in clientObjects){
			GameObject obj = NetworkServer.FindLocalObject (objId);
			NetworkIdentity netIdentity = obj.GetComponent<NetworkIdentity>();

			if (netIdentity.connectionToClient == null){
				netIdentity.RemoveClientAuthority(conn);
				ControlRegistration reg = obj.GetComponent<ControlRegistration> ();
				if (reg != null)
					reg.UnregisterToServer ();
			}
		}

		base.OnServerDisconnect (conn);
	}
}
