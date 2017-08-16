using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using BehaviorDesigner.Runtime;

public class NetworkBehaviorTreeManager : NetworkBehaviour {

	void Start () {
		BehaviorTree behaviorTree = GetComponent<BehaviorTree> ();
		// AI behavior can only run on server, thus disable AI behavior in client
		if (!isServer) {
			behaviorTree.DisableBehavior ();
		}
	}
}
